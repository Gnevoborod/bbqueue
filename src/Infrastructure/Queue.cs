using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace bbqueue.Infrastructure
{
    internal sealed class Queue
    {

       // ConcurrentBag<Ticket> TicketList = new ConcurrentBag<Ticket>();
       List<Ticket> TicketList = new List<Ticket>();
        Dictionary<char, int> TicketAmountList = new Dictionary<char, int>();//коллекция префиксов и максимальных значений у талонов, в разрезе префиксов
        private readonly object obj = new object();
        IServiceProvider serviceProvider;
        public Queue(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        private int AddTicketAmount(char prefix)
        {
            int nextNumber = -1;
            if(TicketAmountList.ContainsKey(prefix))
            {
                nextNumber = ++TicketAmountList[prefix];
            }
            else
            {
                nextNumber = 1;
                TicketAmountList.Add(prefix, nextNumber);
            }
            return nextNumber;
        }

        public int AddTicket(Ticket ticket, char prefix)
        {
            int nextNumber = -1;
            try
            {
                lock (obj)
                {
                    TicketList.Add(ticket);
                    nextNumber = AddTicketAmount(prefix);
                }
                return nextNumber;
            }
            catch
            {
                return nextNumber;
            }
        }

        public bool RemoveTicket(Ticket ticket)
        {
            try
            {
                lock (obj)
                {
                    TicketList.Remove(ticket);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ReturnTicketToQueue(Ticket ticket)
        {
            try
            {
                lock (obj)
                {
                    TicketList.Insert(IndexAvailableForInsertion(),ticket);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private int IndexAvailableForInsertion()
        {
            int offset = 1;
            int result= TicketList.FindLastIndex(a => a.State == TicketState.Returned);
            return result > -1? result + offset : offset;  
        }

        public bool ClearQueue()
        {
            try
            {
                lock (obj)
                {
                    TicketList.Clear();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /*
        public bool RestoreQueue(CancellationToken cancellationToken)
        {
            try
            {
                lock (obj)
                {
                    TicketList = serviceProvider.GetService<ITicketService>()?
                        .LoadTicketsAsync(false, cancellationToken)
                        .Result!;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        */
        public Ticket? GetNextSpecificTicketFromQueue(string ticketNumber)
        {
            lock(obj)
            {
                var ticket = TicketList.FirstOrDefault(a => a.PublicNumber == ticketNumber);
                if (ticket == null)
                    return null;
                ticket.State = TicketState.InProcess;
                return ticket;
            }
        }

        public async Task<Ticket?> GetNextTicketFromQueueAsync(long windowId, CancellationToken cancellationToken)
        {
            //Получаем данные об операциях совершённых с талонами. Нам нужно талоны, доступные для опр.окна
            var tickets = await GetListOfTicketsAsync(windowId);
            Ticket? nextTicket;
            lock (obj)
            {
                //тут надо будет глянуть что в итоге выходит, какой список талонов. возможно потребуется подпилить сортировку, чтобы выдавало по-справедливости.
                nextTicket = (from ticketList in TicketList
                             join ticket in tickets
                             on ticketList.Id equals ticket
                             select ticketList).FirstOrDefault();
                if(nextTicket== null)
                    return null;
                //меняем состояние талона, назначем ему окно (меняем прямо тут, так как это критично - получить и изменить состояние талона при заблокированной очереди
                serviceProvider
                    .GetService<ITicketService>()?
                    .TakeTicketToWork(nextTicket!, windowId);
            }
            return nextTicket;
        }

        private async Task<List<Ticket?>> ListOfTicketsForSpecificWindow(long windowId)
        {
            var tickets = await GetListOfTicketsAsync(windowId);
            List<Ticket?> ticketListForWindow;
            lock (obj)
            {
                ticketListForWindow = (from ticketList in TicketList
                          join ticket in tickets
                          on ticketList.Id equals ticket
                          select ticketList).ToList();
            }
            return ticketListForWindow;
        }


        private async Task<List<long>> GetListOfTicketsAsync(long windowId)
        {
            var ticketOperations = await serviceProvider
                           .GetService<ITicketRepository>()?
                           .GetTicketOperationByWindowPlusTargetAsync(windowId)!;
            List<long> tickets = new();
            foreach (var to in ticketOperations)
            {
                tickets.Add((long)to.TicketId!);
            }
            return tickets;
        }
    }
}
