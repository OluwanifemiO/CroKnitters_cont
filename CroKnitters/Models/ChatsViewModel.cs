using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class ChatsViewModel
    {
        public int ChatId { get; set; } // PChatId from PrivateChat

        public string PartnerUserName { get; set; } // Username of the chat partner

        public int PartnerId { get; set; } // Id of the chat partner

        public string LastMessage { get; set; } // Text from the last message in this chat

        public DateTime LastMessageDate { get; set; } // CreationDate of the last message

        public string? UserImageSrc { get; set; }

        public PrivateChat PrivateChat { get; set; }
    }
}
