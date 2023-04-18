using bbqueue.Database;
using IntegrationTests.BbqueueIntegrations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using NLog;
using System.Diagnostics;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Engine.ClientProtocol;

namespace integrationTests.Controllers
{
    public class TicketControllerTest:IDisposable
    {

        /// <summary>
        /// Проверяем формирование талона
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task GetTicketAsCustomerTestAsync_TicketGot()
        {
            //Без вот этого костыля ниже - тесты падают, поскольку в соседних классах также идёт очистка базы после отработки всех тестов
            //Вероятно есть более элегантное решение, поисками которого я и займусь в ближайшем будущем.
            await Task.Delay(1500);
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var targetCreate = await client.Add_targetAsync(new TargetCreateDto
            {
                Name = "Test",
                Description = "Test",
                Prefix = "T"
            });

            targetCreate.TargetId.ShouldBeGreaterThan(0L);

            var targets = await client.TargetsAsync();
            targets.Targets.Count.ShouldBeGreaterThan(0);

            long? targetId = targets?.Targets?.FirstOrDefault()?.Id;
            targetId.ShouldNotBeNull();

            var result = await client.TicketAsync(targetId);
            result.ShouldNotBeNull();
            result.Id.ShouldBeGreaterThan(0L);
        }

        /// <summary>
        /// Формируем талон и берём его в работу. Перед этим создаём окно, привязываем его к цели и к сотруднику
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task TakeTicketToWorkTestAsyn_TicketTook()
        {
            await Task.Delay(1500);
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var window = await client.Add_windowAsync(new WindowCreateDto
            {
                Number = "П15",
                Description = "Test"
            });

            window.WindowId.ShouldBeGreaterThan(0L);

            var target = await client.Add_targetAsync(new TargetCreateDto
            {
                Name = "Test",
                Description = "Test",
                Prefix = "T"
            });

            target.TargetId.ShouldBeGreaterThan(0L);


            await client.Add_target_windowAsync(new WindowTargetCreateDto
            {
                WindowId = window.WindowId,
                TargetId = target.TargetId
            });

            await client.Employee_to_windowAsync(new EmployeeToWindowDto
            {
                WindowId = window.WindowId
            });

            var ticket = await client.TicketAsync(target.TargetId);
            ticket.Id.ShouldBeGreaterThan(0L);

            var result = await client.NextCustomerAsync();

            result.Id.ShouldBeGreaterThan(0L);
        }


        /// <summary>
        /// А тут талон создаём, берём в работу, и завершаем работу с ним.
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task CloseTicketTestAsync()
        {
            await Task.Delay(1500);
            string jwt = await TestSettings.GetJwtForAdminAsync();
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var window = await client.Add_windowAsync(new WindowCreateDto
            {
                Number = "П15",
                Description = "Test"
            });

            window.WindowId.ShouldBeGreaterThan(0L);

            var target = await client.Add_targetAsync(new TargetCreateDto
            {
                Name = "Test",
                Description = "Test",
                Prefix = "T"
            });

            target.TargetId.ShouldBeGreaterThan(0L);


            await client.Add_target_windowAsync(new WindowTargetCreateDto
            {
                WindowId = window.WindowId,
                TargetId = target.TargetId
            });

            await client.Employee_to_windowAsync(new EmployeeToWindowDto
            {
                WindowId = window.WindowId
            });

            var ticket = await client.TicketAsync(target.TargetId);
            ticket.Id.ShouldBeGreaterThan(0L);

            var ticketToWork = await client.NextCustomerAsync();

            ticketToWork.Id.ShouldBeGreaterThan(0L);

            await client.CloseAsync(new TicketClose
            {
                TicketId = ticketToWork.Id
            });

            //Подключаемся к базе и смоттрим как там дела, так как метода который бы поставил нужную информацию нет
            using (QueueContext queueContext = new QueueContext(true))
            {
                var result = queueContext.TicketEntity.ToList();
                result.Count.ShouldBe(1);
                result.FirstOrDefault()?.State.ShouldBe(bbqueue.Domain.Models.TicketState.Closed);
            }

        }

        /// <summary>
        /// Проверяем корректность работы дашборда интеграционным тестом
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task DashboardTest()
        {
            //await Task.Delay(2500);

            HubConnection connection = new HubConnectionBuilder()
                                            .WithUrl(TestSettings.EndpointAddress + "/dashboard")
                                            .Build();
            
            
            int callCounter = 0;
            connection.On<string>("Refresh",  (message) => {
                message.Length.ShouldBeGreaterThan(0);
                callCounter++;
            });
            await connection.StartAsync();
            connection.ConnectionId.ShouldNotBeNull();
            connection.State.ShouldBe(HubConnectionState.Connected);


            string jwt = await TestSettings.GetJwtForAdminAsync();
            
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwt);
            var client = new BBQueueClient(TestSettings.EndpointAddress, httpClient);

            var window = await client.Add_windowAsync(new WindowCreateDto
            {
                Number = "П15",
                Description = "Test"
            });

            window.WindowId.ShouldBeGreaterThan(0L);

            var target = await client.Add_targetAsync(new TargetCreateDto
            {
                Name = "Test",
                Description = "Test",
                Prefix = "T"
            });

            target.TargetId.ShouldBeGreaterThan(0L);


            await client.Add_target_windowAsync(new WindowTargetCreateDto
            {
                WindowId = window.WindowId,
                TargetId = target.TargetId
            });

            await client.Employee_to_windowAsync(new EmployeeToWindowDto
            {
                WindowId = window.WindowId
            });

            var ticket = await client.TicketAsync(target.TargetId);
            ticket.Id.ShouldBeGreaterThan(0L);

            var ticketToWork = await client.NextCustomerAsync();
            ticketToWork.Id.ShouldBeGreaterThan(0L);

            await client.CloseAsync(new TicketClose
            {
                TicketId = ticketToWork.Id
            });

            //чтобы не перегружать тест логикой - просто считаем сколько раз нам прислали обновление информации по сигналу
            callCounter.ShouldBe(4);
            //Подключаемся к базе и смоттрим как там дела, так как метода который бы поставил нужную информацию нет
            using (QueueContext queueContext = new QueueContext(true))
            {
                var result = queueContext.TicketEntity.ToList();
                result.Count.ShouldBe(1);
                result.FirstOrDefault()?.State.ShouldBe(bbqueue.Domain.Models.TicketState.Closed);
            }
        }
        public async void Dispose()
        {
            try
            {
                using (QueueContext queueContext = new QueueContext(true))
                {
                    queueContext.RemoveRange(queueContext.TicketOperationEntity);
                    queueContext.RemoveRange(queueContext.TicketAmountEntity);
                    queueContext.RemoveRange(queueContext.TicketEntity);
                    queueContext.RemoveRange(queueContext.TargetEntity);
                    queueContext.RemoveRange(queueContext.GroupEntity);
                    queueContext.RemoveRange(queueContext.WindowTargetEntity);
                    queueContext.RemoveRange(queueContext.WindowEntity);
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
