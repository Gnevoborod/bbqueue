using Microsoft.AspNetCore.Mvc;
using bbqueue.Infrastructure.Services;
using bbqueue.Mapper;
using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Interfaces.Services;

namespace bbqueue.Controllers
{
    [Route("api/target")]
    [ApiController]
    public sealed class TargetController : ControllerBase
    {
        private readonly IServiceProvider serviceProvider;

        public TargetController(IServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
        }

        [HttpGet("targets")]
        public async Task<IActionResult> GetTargets(CancellationToken cancellationToken) 
        {
            var targets = await serviceProvider.GetService<ITargetService>()?.GetTargetsAsync(cancellationToken)!;
            cancellationToken.ThrowIfCancellationRequested();//решил ставить проверку на отмену в контроллере после основных манипуляций и в репозитории перед обращением в БД
            TargetListDto targetListDto = new()
            {
                Targets = targets.Select(t => t.FromModelToDto()!).ToList()
            };
            return Ok(targetListDto);
        }

        /// <summary>
        /// Обновлённый метод возвращающий список подразделов и целей
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("hierarchy")]
        public async Task<IActionResult> GetHierarchyAsync(CancellationToken cancellationToken)
        {
            var hierarchy = await new TargetService(serviceProvider).GetHierarchyAsync(cancellationToken);
            cancellationToken.ThrowIfCancellationRequested();
            return Ok(hierarchy);
        }

        /// <summary>
        /// Получаем список разделов\подразделов.
        /// На фронте для выстраивания дерева страниц потребуется вызов и /targets и /groups
        /// </summary>
        /// <returns></returns>
        [HttpGet("groups")]
        public async Task<IActionResult> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var groups = await serviceProvider.GetService<IGroupService>()?.GetGroupsAsync(cancellationToken)!;
            cancellationToken.ThrowIfCancellationRequested();
            GroupListDto groupListDto = new()
            {
                Groups = groups.Select(gld => gld.FromModelToDto()!).ToList()
            };
            return Ok(groupListDto);
        }
    }
}
