using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using bbqueue.Infrastructure.Services;
using bbqueue.Mapper;
using bbqueue.Controllers.Dtos.Target;
using bbqueue.Controllers.Dtos.Group;
using Microsoft.AspNetCore.Authorization;

namespace bbqueue.Controllers
{
    [Route("api/target")]
    [ApiController]
    public class TargetController : ControllerBase
    {
        [HttpGet]
        [Route("targets")]
        public IActionResult GetTargets() 
        {
            var targets = new TargetService().GetTargets();
            TargetListDto targetListDto = new TargetListDto(targets.Count());
            foreach(var target in targets)
            {
                targetListDto?.Targets?.Add(target.FromModelToDto()!);
            }
            return Ok(targetListDto);
        }



        /// <summary>
        /// Получаем список разделов\подразделов.
        /// На фронте для выстраивания дерева страниц потребуется вызов и /targets и /groups
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("groups")]
        public IActionResult GetGroups()
        {
            var groups = new GroupService().GetGroups();
            GroupListDto groupListDto=new GroupListDto(groups.Count());
            foreach(var group in groups)
            {
                groupListDto?.Groups?.Add(group.FromModelToDto()!);
            }
            return Ok(groupListDto);
        }
    }
}
