using bbqueue.Controllers.Dtos.Target;

namespace bbqueue.Controllers.Dtos.Group
{
    internal sealed class GroupHierarchyDto
    {
        public long Id { get; set; }

        public string Name { get; set; } = default!;
        public string? Description { get; set; }

        public List<GroupHierarchyDto>? GroupsInHierarchy { get; set; }
        public List<TargetDto>? Targets { get; set; }

        internal GroupHierarchyDto Father { get; set; } = default!;
    }
}
