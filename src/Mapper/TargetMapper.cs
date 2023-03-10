using bbqueue.Database.Entities;
using bbqueue.Domain.Models;
using System.Runtime.CompilerServices;
using bbqueue.Controllers.Dtos.Target;

namespace bbqueue.Mapper
{
    internal static class TargetMapper
    {
        public static Target FromEntityToModel(this TargetEntity targetEntity)
        {
            if (targetEntity == null)
                return default!;
            return new Target
            {
                Id = targetEntity.Id,
                Name = targetEntity.Name,
                Description = targetEntity.Description,
                Prefix = targetEntity.Prefix,
                GroupLinkId = targetEntity.GroupLinkId,
                GroupLink = targetEntity.GroupLink != null ? new Group
                {
                    Id = targetEntity.GroupLink.Id,
                    Name = targetEntity.GroupLink.Name,
                    Description = targetEntity.GroupLink.Description,
                    GroupLinkId = targetEntity.GroupLink.GroupLinkId,
                    GroupLink = null /* Stop recursive mapping */
                } : null
            };
        }

        public static TargetEntity FromModelToEntity(this Target target)
        {
            if (target == null)
                return default!;
            return new TargetEntity
            {
                Id = target.Id,
                Name = target.Name,
                Description = target.Description,
                Prefix = target.Prefix,
                GroupLinkId = target.GroupLinkId,
                GroupLink = target.GroupLink != null ? new GroupEntity
                {
                    Id = target.GroupLink.Id,
                    Name = target.GroupLink.Name,
                    Description = target.GroupLink.Description,
                    GroupLinkId = target.GroupLink.GroupLinkId,
                    GroupLink = null! /* Stop recursive mapping */
                } : null!
            };
        }

        public static TargetDto FromModelToDto(this Target target)
        {
            if (target == null)
                return default!;
            return new TargetDto
            {
                Id = target.Id,
                Name = target.Name,
                Description = target.Description,
                Prefix = target.Prefix,
                GroupLinkId = target.GroupLinkId
            };
        }
    }
}
