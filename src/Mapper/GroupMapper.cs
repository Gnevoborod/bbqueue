using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace bbqueue.Mapper
{
    internal static class GroupMapper
    {
        public static Group? FromEntityToModel(this GroupEntity? groupEntity)
        {
            if (groupEntity == null)
                return null;
            return new Group
            {
                Id = groupEntity.Id,
                Name = groupEntity.Name,
                Description = groupEntity.Description,
                GroupLinkId = groupEntity.GroupLinkId,//вероятно не нужно
                GroupLink = FromEntityToModel(groupEntity.GroupLink) /* Мы так можем делать? ParentGroup*/
            };
        }

        public static GroupEntity? FromModelToEntity(this Group? group)
        {
            if (group == null)
                return null;
            return new GroupEntity
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                GroupLinkId = group.GroupLinkId,
                GroupLink = FromModelToEntity(group.GroupLink) /* Мы так можем делать? */
            };
        }
    }
}
