using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class MessageViewModel
    {
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public string Content { get; set; }

        public DateTime SentTime { get; set; }

        public User senderInfo {  get; set; }

        public User receiverInfo {  get; set; }
    }
}
