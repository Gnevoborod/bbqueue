using Microsoft.AspNetCore.Mvc;
using bbqueue.Mapper;
using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Group;
using bbqueue.Domain.Interfaces.Services;
using bbqueue.Controllers.Dtos.Error;
using bbqueue.Infrastructure.Exceptions;
using Microsoft.AspNetCore.Authorization;
using System.Data;

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


        /// <summary>
        /// Создаёт новую цель
        /// </summary>
        /// <param name="targetCreateDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(TargetCreatedIdDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpPost("add_target")]
        public async Task<IActionResult> AddTargetAsync([FromBody]TargetCreateDto targetCreateDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var targetId = await targetService.AddTargetAsync(targetCreateDto.FromDtoToModel(), cancellationToken);
            return Ok(new TargetCreatedIdDto()
            {
                TargetId = targetId
            });
        }

        /// <summary>
        /// Создаёт новый раздел\подраздел
        /// </summary>
        /// <param name="groupCreateDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Manager")]
        [ProducesResponseType(typeof(GroupCreatedIdDto),200)]
        [ProducesResponseType(typeof(ErrorDto), 400)]
        [HttpPost("add_group")]
        public async Task<IActionResult> AddGroupAsync([FromBody]GroupCreateDto groupCreateDto)
        {
            CancellationToken cancellationToken = HttpContext.RequestAborted;
            var groupId = await groupService.AddGroupAsync(groupCreateDto.FromDtoToModel(), cancellationToken);
            return Ok(new GroupCreatedIdDto()
            {
                GroupId = groupId
            });
        }
    }
}
