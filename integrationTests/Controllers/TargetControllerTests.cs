using IntegrationTests.BbqueueIntegrations;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace integrationTests.Controllers
{
    [TestCaseOrderer(
    ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer",
    ordererAssemblyName: "XUnit.Project")]
    public class TargetControllerTests
    {
        /// <summary>
        /// Создаём простые разделы, без разделов предков
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("Овощи","Лавка с овощами")]
        [InlineData("Фрукты", "Лавка с фруктами")]
        [InlineData("Мясо", "Мясной отдел")]
        public async Task ACreateGroupsTestAsync_NoRelations(string name, string description)
        {
            //создаём ряд групп без привязки к предыдущим группам
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var result = await client.Add_groupAsync(new GroupCreateDto
            {
                Name = name,
                Description = description
            });
            result.GroupId.ShouldBeGreaterThan(0L);
            
        }

        /// <summary>
        /// Создаём сложные разделы, с предками
        /// </summary>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("Свинина", "Сложную логику не закладываем")]
        [InlineData("Говядина", "Сложную логику не закладываем")]
        public async Task BCreateGroupsTestAsync_WithRelations(string name, string description)
        {
            //создаём ряд групп c привязкой к предыдущим группам
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var groupLink = await client.GroupsAsync();

            var result = await client.Add_groupAsync(new GroupCreateDto
            {
                Name = name,
                Description = description,
                GroupLinkId = groupLink == null ? default! : groupLink?.Groups?.SingleOrDefault(g=>g.Name.Equals("Мясо"))?.Id
            });

            result.GroupId.ShouldBeGreaterThan(0L);
        }

        [Theory]
        [InlineData("Шашлык","Шашлыка заточить")]
        [InlineData("Жаркое", "Жаркое под прохладное")]
        public async Task CCreateTargetsTestAsync_WithRelations(string name, string description)
        {
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var groupLink = await client.GroupsAsync();

            var result = await client.Add_targetAsync(new TargetCreateDto
            {
                Name = name,
                Description = description,
                GroupLinkId = groupLink == null ? default! : groupLink?.Groups?.SingleOrDefault(g => g.Name.Equals("Мясо"))?.Id,
                Prefix = name[0].ToString()
            });

            result.TargetId.ShouldBeGreaterThan(0L);
        }
    }
}
