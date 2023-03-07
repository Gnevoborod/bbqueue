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
        private readonly ITargetService targetService;
        private readonly IGroupService groupService;

        public TargetController(ITargetService targetService, IGroupService groupService)
        {
            this.targetService = targetService;
            this.groupService = groupService;
        }

        [HttpGet("targets")]
        public async Task<IActionResult> GetTargets() 
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var targets = await targetService.GetTargetsAsync(cancellationToken)!;
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
        public async Task<IActionResult> GetHierarchyAsync()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var hierarchy = await targetService.GetHierarchyAsync(cancellationToken)!;
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
            var groups = await groupService.GetGroupsAsync(cancellationToken)!;
            GroupListDto groupListDto = new()
            {
                Groups = groups.Select(gld => gld.FromModelToDto()!).ToList()
            };
            return Ok(groupListDto);
        }
    }
}
