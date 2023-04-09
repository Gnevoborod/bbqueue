using IntegrationTests.BbqueueIntegrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace integrationTests
{
    internal class TestSettings
    {
        public const string EndpointAddress = "http://62.173.154.32:5005";
        private static string JWT;
        public static async Task<string> GetJwtForEmployeeAsync(string employeeExtId)
        {
            var client = new BBQueueClient(EndpointAddress, new HttpClient());

            var employeeJWT = await client.JwtAsync(employeeExtId);

            return employeeJWT.Token.ToString();
        }

        public static async Task<string> GetJwtForAdminAsync()
        {
            if(JWT != null)
               return JWT; 
            var client = new BBQueueClient(EndpointAddress, new HttpClient());

            var employeeJWT = await client.JwtAsync("admin");
            JWT = employeeJWT.Token.ToString();
            return JWT;
        }
    }
}
