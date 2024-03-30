using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CroKnitters.Controllers
{
    public class ProfileController : Controller
    { 
        public ProfileController(CrochetAppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            string userId = Request.Cookies["userId"];

            if (!string.IsNullOrEmpty(userId))
            {
                int userIdValue;
                if (int.TryParse(userId, out userIdValue))
                {
                    User user = _db.Users.FirstOrDefault(m => m.UserId == userIdValue);

                    if (user != null)
                    {
                        string userImageSrc = null;
                        int userImageId = 0;

                        // Check if the user has an ImageId
                        if (user.ImageId.HasValue)
                        {
                            // Find the image by ImageId and get the ImageSrc
                            var image = _db.Images.FirstOrDefault(i => i.ImageId == user.ImageId.Value);
                            if (image != null)
                            {
                                userImageSrc = image.ImageSrc;
                                userImageId = image.ImageId;
                            }
                        }

                        //get the number of comments, patterns, projects and groups
                        var commentsMade = _db.Comments.Where(c => c.OwnerId == userIdValue).Count();

                        var patternsOwned = _db.Patterns.Count(c => c.OwnerId == userIdValue);

                        var projectsOwned = _db.Projects.Count(c => c.OwnerId == userIdValue);

                        var groupsJoined = _db.GroupUsers.Count(c => c.UserId == userIdValue);

                        // Prepare the view model
                        UserProfileViewModel viewModel = new UserProfileViewModel()
                        {
                            user = user,
                            UserImageSrc = userImageSrc, 
                            numberofComments = commentsMade,
                            numberofPatterns = patternsOwned,
                            numberofProjects = projectsOwned,
                            numOfGroups = groupsJoined
                        };
                        return View("Index", viewModel);
                    }
                }
            }
            // If any of the cookies are missing, redirect to login

            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult EditProfile(int id)
        {
            int userId = id;
            User user = _db.Users.Find(userId);

            string userImageSrc = null;
            int userImageId = 0;

            // Check if the user has an ImageId
            if (user.ImageId.HasValue)
            {
                // Find the image by ImageId and get the ImageSrc
                var image = _db.Images.FirstOrDefault(i => i.ImageId == user.ImageId.Value);
                if (image != null)
                {
                    userImageSrc = image.ImageSrc;
                    userImageId = image.ImageId;
                }
            }

            // Prepare the view model
            EditProfileViewModel viewModel = new EditProfileViewModel()
            {
                UserId = id,
                 FirstName = user.FirstName,
                 LastName = user.LastName,
                 Username = user.Username,
                 Email = user.Email,
                 Bio = user.Bio, 
                 Password = user.Password,
                 ImageSrc = userImageSrc,
                 ImageId = userImageId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(EditProfileViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                // Log or inspect ModelState errors
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                    }
                }

                Console.WriteLine("something went wrong");
                return View(userViewModel);
            }

            Image newImage = null;

            if (userViewModel.UserImageSrc != null)
            {   
                //retrieve the profile picture if there is one
                var imageName = userViewModel.UserImageSrc;

                //get the name
                var fileName = Path.GetFileName(imageName.FileName);

                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\profile", fileName);
                Console.WriteLine("filepath for current picture: " + filePath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageName.CopyTo(stream);

                    Console.WriteLine(stream);
                }

                 newImage = new Image { ImageSrc = fileName };
                _db.Images.Add(newImage);
            }

            User updateUser = new User()
            {
                UserId = userViewModel.UserId,
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Username = userViewModel.Username,
                Bio = userViewModel.Bio,
                Password = userViewModel.Password
            };

            // Check if a new image was uploaded and associate it with the user
            if (userViewModel.UserImageSrc != null)
            {
                updateUser.ImageId = newImage.ImageId;
                updateUser.Image = newImage;
            }
            else
            {
                updateUser.ImageId = userViewModel.ImageId;
            }

            _db.Users.Update(updateUser);
            _db.SaveChanges();

            return RedirectToAction("Index", "Profile");
        }

        private CrochetAppDbContext _db;

      
    }
}
