using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using Microsoft.EntityFrameworkCore;
namespace bbqueue.Mapper
{
    internal static class GroupMapper
    {
        public static Group? FromEntityToModel(GroupEntity groupEntity)
        {
            if (groupEntity == null)
                return null;
            return new Group
            {
                Id = groupEntity.Id,
                Name = groupEntity.Name,
                Description = groupEntity.Description,
                GroupLinkId = groupEntity.GroupLinkId,
                GroupLink = FromEntityToModel(groupEntity.GroupLink) /* Мы так можем делать? */
            };
        }

        public static GroupEntity? FromModelToEntity(Group group)
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
