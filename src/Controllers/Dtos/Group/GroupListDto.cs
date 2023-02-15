namespace bbqueue.Controllers.Dtos.Group
{
    internal sealed class GroupListDto
    {
        public List<GroupDto>? Groups { get; private set; }
        public GroupListDto(int count)
        {
            Groups = new List<GroupDto>(count);
        }
    }
}
