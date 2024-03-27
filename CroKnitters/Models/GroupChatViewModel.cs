using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class GroupChatViewModel
    {
        public List<GroupUser> groupMembers {  get; set; } //list of the members in the group

        public int GroupId {  get; set; }

        public string? UserImageSrc {  get; set; }

        //message details
        public List<MessageViewModel> viewModel {  get; set; }

    }
}
