using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class EditProfileViewModel
    {
        public int UserId {  get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Bio { get; set; }

        public string? Password { get; set; }

        //public string? City { get; set; }

        public string? Province { get; set; }

        public IFormFile? UserImageSrc { get; set; }

        public string? ImageSrc { get; set; }

        public int? ImageId { get; set; }
    }
}
