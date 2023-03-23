using Microsoft.AspNetCore.Mvc.Razor;
using static bbqueue.Infrastructure.Exceptions.ExceptionEvents;

namespace bbqueue.Infrastructure.Exceptions
{
    public class ApiException : Exception
    {
        public ApiException(EventId eventId) : base(message: eventId.Name)
        {
            HResult = eventId.Id;
        }

        public ApiException(EventId eventId, string args) : base(message: eventId.Name + " " + args)
        {
            HResult = eventId.Id;
        }
    }
}
