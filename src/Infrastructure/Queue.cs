using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;

namespace bbqueue.Infrastructure
{
    internal sealed class Queue
    {
        List<Ticket> TicketList = new List<Ticket>();
        private readonly object obj = new object();
        IServiceProvider serviceProvider;
        public Queue(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public bool AddTicket(Ticket ticket)
        {
            try
            {
                lock (obj)
                {
                    TicketList.Add(ticket);
                }
                return true;
            }
            catch
            {
                return false;
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

        public Ticket? GetNextTicketFromQueue(long windowId)
        {
            lock (obj)
            {
                //тут нужно проработать логику поиска всех талонов которые может обрабатывать конкретное окно
                //потребуется отбирать через target + window_target и по префиксу искать, или иначе как-то.
            }
            return new();
        }

        private List<Ticket> ListOfTicketsForSpecificWindow(long windowId)
        {
            return new();
        }
    }
}
