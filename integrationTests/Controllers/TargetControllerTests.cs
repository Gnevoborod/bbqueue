using bbqueue.Database;
using IntegrationTests.BbqueueIntegrations;
using Shouldly;
using System.Net.Http.Headers;

namespace integrationTests.Controllers
{
    public class TargetControllerTests:IDisposable
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
        public async Task CreateGroupsTestAsync_NoRelations(string name, string description)
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
        public async Task CreateGroupsTestAsync_WithRelations(string name, string description)
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
        public async Task CreateTargetsTestAsync_WithRelations(string name, string description)
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

        public async void Dispose()
        {
            try
            {
                using (QueueContext queueContext = new QueueContext(true))
                {
                    queueContext.RemoveRange(queueContext.TargetEntity);
                    queueContext.RemoveRange(queueContext.GroupEntity);
                    await queueContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
