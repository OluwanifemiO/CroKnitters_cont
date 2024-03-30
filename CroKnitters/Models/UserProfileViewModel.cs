using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class UserProfileViewModel
    {
        public User user { get; set; }

        //public string? City { get; set; }

        public string? Province { get; set; }

        public string? UserImageSrc {  get; set; }

        public int? numOfGroups {  get; set; }

        public int? numberofPatterns { get; set; }

        public int? numberofProjects { get; set; }

        public int? numberofComments { get; set; }
    }
}
