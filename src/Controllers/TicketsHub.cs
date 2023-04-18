using bbqueue.Controllers.Dtos.Ticket;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Mapper;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace bbqueue.Controllers
{
    public class TicketsHub:Hub
    {
        private readonly ITicketService ticketService = default!;
        private readonly ILogger<TicketsHub> logger;

        public TicketsHub(ITicketService ticketService, ILogger<TicketsHub> logger)
        {
            this.ticketService = ticketService;
            this.logger = logger;
        }
        
        public override async Task OnConnectedAsync()
        {
            await ticketService.RefreshOnlineQueueAsync();
        }

    }
}
