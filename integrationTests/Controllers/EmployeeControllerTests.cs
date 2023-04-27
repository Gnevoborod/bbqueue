using IntegrationTests.BbqueueIntegrations;
using Shouldly;
using System.Net.Http.Headers;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
[assembly: TestCaseOrderer(
     ordererTypeName: "integrationTests.AlphabeticalOrderer",
    ordererAssemblyName: "integrationTests")]
namespace integrationTests.Controllers
{
    public class EmployeeTests
    {
        /// <summary>
        /// Это просто самый первый метод который я реализовал в рамках интеграционных тестов. Не стал его стирать.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetEmployeeTestAsync_NameIsAdmin()
        {

            string jwt = await TestSettings.GetJwtForAdminAsync();

            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);

            var client = new BBQueueEmployeeServiceClient(TestSettings.EmployeeEndpointAddress, httpClient);
        
            var result = await client.EmployeeAsync(1);

            result.Name.ShouldBe("admin");
        }
    }
}