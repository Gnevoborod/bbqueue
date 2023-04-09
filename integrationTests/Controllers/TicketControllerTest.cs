using IntegrationTests.BbqueueIntegrations;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace integrationTests.Controllers
{
    [TestCaseOrderer(
    ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer",
    ordererAssemblyName: "XUnit.Project")]
    public class TicketControllerTest
    {

        /// <summary>
        /// Сначала формируем талон
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AGetTicketAsCustomerTestAsync_TicketGot()
        {
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var targets = await client.TargetsAsync();

            long? targetId = targets?.Targets?.FirstOrDefault()?.Id;
            targetId.ShouldNotBeNull();

            var result = await client.TicketAsync(targetId);
            result.ShouldNotBeNull();
            result.Id.ShouldBeGreaterThan(0L);
        }

        /// <summary>
        /// Затем берём талон в работу
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task BTakeTicketToWorkTestAsyn_TicketTook()
        {
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var result = await client.NextCustomerAsync();

            result.Id.ShouldBeGreaterThan(0L);
        }


    }
}
