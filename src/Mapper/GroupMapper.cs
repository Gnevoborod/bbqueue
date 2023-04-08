using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
using bbqueue.Controllers.Dtos.Group;
using System.Linq;

namespace bbqueue.Mapper
{
    internal static class GroupMapper
    {
        public static Group FromEntityToModel(this GroupEntity groupEntity)
        {
            if (groupEntity == null)
                return default!;
            return new Group
            {
                Id = groupEntity.Id,
                Name = groupEntity.Name,
                Description = groupEntity.Description,
                GroupLinkId = groupEntity.GroupLinkId,
                GroupLink = groupEntity.GroupLink == null? null : FromEntityToModel(groupEntity.GroupLink)
            };
        }

        public static GroupEntity FromModelToEntity(this Group group)
        {
            if (group == null)
                return default!;
            return new GroupEntity
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                GroupLinkId = group.GroupLinkId,
                GroupLink = group.GroupLink == null? null! : FromModelToEntity(group.GroupLink)
            };
        }

        public static GroupDto FromModelToDto(this Group group)
        {
            if (group == null)
                return default!;
            return new GroupDto
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                GroupLinkId = group.GroupLinkId
            };
        }

        public static GroupHierarchyDto FromModelToHierarchyDto(this List<Group> group, List<Target> target, GroupDto groupsDto=default!, long parentId=-1)
        {
            GroupHierarchyDto groupHierarchyDto = new GroupHierarchyDto();

            groupHierarchyDto.Name = (groupsDto?.Name)?? "";
            groupHierarchyDto.Description = (groupsDto?.Description) ?? "";
            groupHierarchyDto.Id = (groupsDto?.Id)??-1;
            groupHierarchyDto.GroupsInHierarchy = new List<GroupHierarchyDto>();
            var groupsToProcess = group.Where(g => g.GroupLinkId == (parentId > -1 ? parentId : groupsDto?.GroupLinkId));
            var groupsDtoToProcess = groupsToProcess.Select(g => g.FromModelToDto()).ToList();
            var groupToNextProcess = group.Except(groupsToProcess).ToList();
            for(int i = 0; i < groupsDtoToProcess.Count;i++)
            {
                groupHierarchyDto.GroupsInHierarchy.Add(groupToNextProcess.FromModelToHierarchyDto(target, groupsDtoToProcess[i], groupsDtoToProcess[i].Id)) ;
            }
            groupHierarchyDto.Targets = target.Where(t => t.GroupLinkId == (parentId > -1 ? parentId : (groupsDto?.GroupLinkId) ?? null)).Select(t => t.FromModelToDto()).ToList();

            return groupHierarchyDto;
        }

        public static Group FromDtoToModel(this GroupCreateDto groupCreateDto)
        {
            if (groupCreateDto == null)
                return default!;
            return new Group
            {
                Name = groupCreateDto.Name,
                Description = groupCreateDto.Description,
                GroupLinkId = groupCreateDto.GroupLinkId
            };
        }
    }
}
