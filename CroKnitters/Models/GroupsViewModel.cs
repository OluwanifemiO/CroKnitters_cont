using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class GroupsViewModel
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public int MemberCount { get; set; }

        public List<Group> groups { get; set; }
    }
}
