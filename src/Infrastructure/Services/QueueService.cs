using bbqueue.Domain.Interfaces.Repositories;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Domain.Models;
using bbqueue.Mapper;

namespace bbqueue.Infrastructure.Services
{
    public class QueueService:IQueueService
    {
        ITicketRepository ticketRepository;

        public QueueService(ITicketRepository ticketRepository)
        {
            this.ticketRepository = ticketRepository;
        }

        public Task ClearQueueAsync(CancellationToken cancellationToken) 
        {
            return ticketRepository.DeleteAllTicketsFromDBAsync(cancellationToken);
        }

        public Task<Ticket?> GetTicketNextTicketFromQueueAsync(long employeeId, CancellationToken cancellationToken) 
        {
            return ticketRepository.GetNextTicketAsync(employeeId, cancellationToken);
        }

        public Task<Ticket?> GetNextSpecificTicketFromQueueAsync(long ticketNumber, long employeeId, CancellationToken cancellationToken)
        { 
            return ticketRepository.GetNextSpecificTicketAsync(ticketNumber, employeeId, cancellationToken);
        }
    }
}
