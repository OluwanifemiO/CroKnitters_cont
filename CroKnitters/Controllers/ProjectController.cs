using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CroKnitters.Controllers
{
    public class ProjectController : Controller
    {
        public ProjectController(CrochetAppDbContext crochetDbContext)
        {
            _crochetDbContext = crochetDbContext;
        }

        public IActionResult Index()
        {
            int userId = Int32.Parse(Request.Cookies["userId"]!);

            var allProjects = _crochetDbContext.Projects.Include(p => p.ProjectImages)
                .Include(p => p.ProjectComments)
                .Include(p => p.Owner)
                .OrderBy(p => p.ProjectName)
                .Select(p => new ProjectSummaryViewModel()
                {
                    ActiveProject = p,
                    NumberOfComments = p.ProjectComments.Count(),
                    Images = p.ProjectImages.Select(pi => pi.Image.ImageSrc).ToList()
                }).Where(p => p.ActiveProject.OwnerId == userId).ToList();

            if (allProjects == null || allProjects.Count() == 0)
            {
                TempData["noProject"] = "No projects available";
            }
            return View(allProjects);
        }

        [HttpGet]
        public IActionResult CreateNewProject()
        {
            return View("CreateProject");
        }

        //accept the request containing all the relevant details
        [HttpPost]
        public async Task<IActionResult> CreateProject(ProjectViewModel projectViewModel)
        {
            TempData["manyImages"] = "";
            //get the current user id
            int userId = Int32.Parse(Request.Cookies["userId"]!);
            //ModelState.Remove("User");

            //check if there are no errors
            if (ModelState.IsValid)
            {

                //valid user
                if (userId != 0)
                {
                    projectViewModel.ActiveProject.CreationDate = DateTime.Now;
                    projectViewModel.ActiveProject.OwnerId = userId;
                    projectViewModel.ActiveProject.Owner = _crochetDbContext.Users.FirstOrDefault(o => o.UserId == userId);

                    UploadImages(projectViewModel);

                    //create a new tag for the pattern
                    if (projectViewModel.Tags != null)
                    {

                        //should split the tagname if there are more than one i.e tag1, tag2, tag3
                        var splitTags = projectViewModel.Tags.Split(", ").Select(t => t.Trim());

                        foreach (var tag in splitTags)
                        {
                            //check if it exists
                            var existingTag = _crochetDbContext.Tags.FirstOrDefault(t => t.TagName == tag);

                            // Tag doesn't exist, create a new one                       
                            if (existingTag == null)
                            {
                                existingTag = new Tag { TagName = projectViewModel.Tags };
                                _crochetDbContext.Tags.Add(existingTag);
                            }
                            else
                            {
                                // Tag already exists, associate with the pattern
                                projectViewModel.ActiveProject.ProjectTags.Add(new ProjectTag { Tag = existingTag });
                            }
                        }
                    }

                    //add the project
                    _crochetDbContext.Projects.Add(projectViewModel.ActiveProject);
                    //save
                    await _crochetDbContext.SaveChangesAsync();

                    TempData["LastActionMessage"] = $"New Project \"{projectViewModel.ActiveProject.ProjectName}\" was added.";
                    return RedirectToAction("Index", "Project");
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
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
                return View("CreateProject", projectViewModel);
            }

        }

        public void UploadImages(ProjectViewModel viewModel)
        {
            //handle image upload
            //check if there are images in the collection and they are not more than 2
            if (viewModel.Images != null && viewModel.Images.Count > 0 && viewModel.Images.Count <= 2)
            {
                //loop through the collection
                foreach (var image in viewModel.Images)
                {
                    var fileName = Path.GetFileName(image.FileName);

                    var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "projects");
                    var filePath = Path.Combine(directoryPath, fileName);
                    Console.WriteLine("filepath for current picture: " + filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        image.CopyTo(stream);

                        Console.WriteLine(stream);
                    }

                    //var imageSrc = Path.Combine("wwwroot/img/projects", fileName);
                    var newImage = new Image { ImageSrc = fileName };
                    _crochetDbContext.Images.Add(newImage);
                    //var img = viewModel.ActiveProject.ProjectImages.Select(i => i.Image = newImage);
                    viewModel.ActiveProject.ProjectImages.Add(new ProjectImage { Image = newImage });
                }

            }
            else
            {
                TempData["manyImages"] = "You can't upload more than 2 images";
            }
        }


        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var projectViewModel = await _crochetDbContext.Projects
                .Where(p => p.ProjectId == id)
                .Include(p => p.ProjectImages).ThenInclude(pi => pi.Image)
                .Include(p => p.ProjectComments)
                .Include(p => p.ProjectTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync();

            if (projectViewModel == null)
            {
                return NotFound();
            }

            // Check if the Owner is null
            if (projectViewModel.Owner == null)
            {
                // Fetch the owner details separately
                var owner = await _crochetDbContext.Users
                    .Where(u => u.UserId == projectViewModel.OwnerId)
                    .FirstOrDefaultAsync();

                // If owner is still null, handle accordingly (e.g., return an error view)
                if (owner == null)
                {
                    return NotFound("Owner not found");
                }

                projectViewModel.Owner = owner;
            }

            //get the tags associated with the pattern
            var tags = _crochetDbContext.ProjectTags.Where(pt => pt.ProjectId == projectViewModel.ProjectId).Select(pt => pt.Tag.TagName).ToList();

            //join them
            var joinedTags = string.Join(", ", tags.Select(t => t.Trim()));

            // Continue building the ViewModel
            var projectDetailViewModel = new ProjectDetailViewModel
            {
                ActiveProject = projectViewModel,
                TagNames = joinedTags,
                Images = projectViewModel.ProjectImages
                    .Where(pi => pi.Image != null)
                    .Select(pi => pi.Image.ImageSrc)
                    .ToList(),
                Owner = projectViewModel.Owner.FirstName + " " + projectViewModel.Owner.LastName
            };

            return View("ProjectDetails", projectDetailViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> EditProject(int id)
        {
            //find the pattern with the id
            var existingProject = await _crochetDbContext.Projects
                .Include(p => p.ProjectTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.ProjectImages)
                .FirstOrDefaultAsync(p => p.ProjectId == id);

            //if the project exists
            if (existingProject != null)
            {
                var tags = existingProject.ProjectTags.Select(pt => pt.Tag.TagName).ToList();
                var tagName = String.Join(", ", tags);

                ProjectViewModel viewModel = new ProjectViewModel()
                {
                    ActiveProject = existingProject,
                    Tags = tagName
                };

                return View(viewModel);
            }
            else return NotFound();

        }

        public void UpdateImages(ProjectViewModel viewModel)
        {
            //handle image edit
            //check if there are images in the collection and they are not more than 2
            if (viewModel.Images != null && viewModel.Images.Count > 0 && viewModel.Images.Count <= 2)
            {
                //loop through the collection
                foreach (var image in viewModel.Images)
                {
                    //get the name
                    var fileName = Path.GetFileName(image.FileName);

                    //check for the image in the db
                    var imageModel = _crochetDbContext.Images.Where(i => i.ImageSrc == fileName);

                    //if it exists, 
                    if (imageModel != null)
                    {
                        Console.WriteLine("Image exists!");
                        TempData["ImageExists"] = "Image already exists";
                    }
                    else //if it doesn't, create a new one
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\img\\patterns", fileName);
                        Console.WriteLine("filepath for current picture: " + filePath);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            image.CopyTo(stream);

                            Console.WriteLine(stream);
                        }


                        //var imageSrc = Path.Combine("wwwroot/img/patterns", fileName);
                        var newImage = new Image { ImageSrc = fileName };
                        _crochetDbContext.Images.Add(newImage);
                        //var img = viewModel.ActivePattern.PatternImages.Select(i => i.Image = newImage);
                        viewModel.ActiveProject.ProjectImages.Add(new ProjectImage { Image = newImage });
                    }
                }


            }
            else
            {
                TempData["manyImages"] = "You can't upload more than 2 images";
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditProject(ProjectViewModel projectViewModel)
        {
            if (ModelState.IsValid)
            {
                //find the project with the id
               var existingProject = await _crochetDbContext.Projects
                    .Include(p => p.ProjectTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.ProjectImages)
                    .FirstOrDefaultAsync(p => p == projectViewModel.ActiveProject);

                if (existingProject == null)
                {
                    Console.WriteLine("Didnt find project.");
                    return NotFound();
                }

                UpdateImages(projectViewModel);
                // Clear existing tags
                //existingProject.ProjectTags.Clear();

                var splitTags = projectViewModel.Tags.Split(", ").Select(t => t.Trim());
                // Iterate through the tags in the view model and update tags
                foreach (var tagName in splitTags)
                {
                    if (!string.IsNullOrEmpty(tagName))
                    {
                        // Check if the tag already exists
                        var existingTag = _crochetDbContext.Tags.FirstOrDefault(t => t.TagName == tagName);

                        if (existingTag == null)
                        {
                            // If the tag doesn't exist, create a new one
                            existingTag = new Tag { TagName = projectViewModel.Tags };
                            _crochetDbContext.Tags.Add(existingTag);
                        }

                        // Create a new ProjectTag and associate it with the pattern and tag
                        existingProject.ProjectTags.Add(new ProjectTag {Project = existingProject, Tag = existingTag });
                    }
                }
                // Save changes to the database
                await _crochetDbContext.SaveChangesAsync();
                return RedirectToAction("Details", new { id = existingProject.ProjectId });
            }

            // Log or inspect ModelState errors
            foreach (var key in ModelState.Keys)
            {
                var state = ModelState[key];
                foreach (var error in state.Errors)
                {
                    Console.WriteLine($"Key: {key}, Error: {error.ErrorMessage}");
                }
            }
            Console.WriteLine("Something went wrong!");
            return View(projectViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteProject(int id, ProjectViewModel projectViewModel)
        {
            // Find the project
            var project = await _crochetDbContext.Projects
                    .Include(p => p.ProjectTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.ProjectImages)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            projectViewModel = new ProjectViewModel()
            {
                ActiveProject = project
            };
            return View(projectViewModel);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the project
            var project = await _crochetDbContext.Projects
                    .Include(p => p.ProjectTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.ProjectImages)
                    .FirstOrDefaultAsync(p => p.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            // Manually remove related entities
            _crochetDbContext.ProjectTags.RemoveRange(project.ProjectTags);
            _crochetDbContext.ProjectImages.RemoveRange(project.ProjectImages);

            // Remove the project
            _crochetDbContext.Projects.Remove(project);
            await _crochetDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private CrochetAppDbContext _crochetDbContext;

    }
}
