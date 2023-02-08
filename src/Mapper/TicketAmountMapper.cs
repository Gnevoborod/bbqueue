using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
namespace bbqueue.Mapper
{
    internal static class TicketAmountMapper
    {
        public static TicketAmount? FromEntityToModel(this TicketAmountEntity ticketAmountEntity)
        {
            if (ticketAmountEntity == null)
                return null;
            return new TicketAmount
            {
                Id = ticketAmountEntity.Id,
                Number = ticketAmountEntity.Number,
                Prefix = ticketAmountEntity.Prefix
            };
        }

        public static TicketAmountEntity? FromModelToEntity(this TicketAmount ticketAmount)
        {
            if (ticketAmount == null)
                return null;
            return new TicketAmountEntity
            {
                Id = ticketAmount.Id,
                Number = ticketAmount.Number,
                Prefix = ticketAmount.Prefix
            };
        }
    }
}
