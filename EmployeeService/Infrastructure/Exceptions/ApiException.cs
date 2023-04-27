namespace EmployeeService.Infrastructure.Exceptions
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
