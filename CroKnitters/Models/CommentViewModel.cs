using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class CommentViewModel
    {
        public Comment ActiveComment { get; set; }

        public int? ProjectId { get; set; }

        public int? PatternId { get; set; }

    }
}
