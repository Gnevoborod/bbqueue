using Microsoft.AspNetCore.Mvc;
using bbqueue.Mapper;
using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Controllers.Dtos.Error;

namespace bbqueue.Controllers
{
    [Route("api/target")]
    [Produces("application/json")]
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

        /// <summary>
        /// Поставляет список целей
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(TargetListDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpGet("targets")]
        public async Task<IActionResult> GetTargets() 
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var targets = await targetService.GetTargetsAsync(cancellationToken);
            TargetListDto targetListDto = new()
            {
                Targets = targets.Select(t => t.FromModelToDto()).ToList()
            };
            return Ok(targetListDto);
        }

        /// <summary>
        /// Возвращает список подразделов и целей
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(GroupHierarchyDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpGet("hierarchy")]
        public async Task<IActionResult> GetHierarchyAsync()
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var hierarchy = await targetService.GetHierarchyAsync(cancellationToken);
            return Ok(hierarchy);
        }

        /// <summary>
        /// Поставляет список разделов и подразделов.
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(GroupListDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpGet("groups")]
        public async Task<IActionResult> GetGroupsAsync(CancellationToken cancellationToken)
        {
            var groups = await groupService.GetGroupsAsync(cancellationToken);
            GroupListDto groupListDto = new()
            {
                Groups = groups.Select(gld => gld.FromModelToDto()).ToList()
            };
            return Ok(groupListDto);
        }
    }
}
