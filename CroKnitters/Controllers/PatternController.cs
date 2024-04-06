using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CroKnitters.Controllers
{
    public class PatternController : Controller
    {
        public PatternController(CrochetAppDbContext crochetDbContext)
        {
            _crochetDbContext = crochetDbContext;
        }

        public IActionResult Index()
        {
            int userId = Int32.Parse(Request.Cookies["userId"]!);

            var allPatterns = _crochetDbContext.Patterns.Include(p => p.PatternImages)
                .Include(p => p.PatternComments)
                .Include(p => p.Owner)
                .OrderBy(p => p.PatternName)
                .Select(p => new PatternSummaryViewModel()
                {
                    ActivePattern = p,
                    NumberOfComments = p.PatternComments.Count(),
                    Images = p.PatternImages.Select(pi => pi.Image.ImageSrc).ToList()
                }).Where(p => p.ActivePattern.OwnerId == userId).ToList();

            if (allPatterns == null || allPatterns.Count == 0)
            {
                TempData["noPattern"] = "No patterns available";
            }
            return View(allPatterns);
        }

        [HttpGet]
        public IActionResult CreateNewPattern()
        {
            TempData["manyImages"] = null;
            return View("CreatePattern");
        }


        //accept the request containing all the relevant details
        [HttpPost]
        public async Task<IActionResult> CreatePattern(PatternViewModel patternViewModel)
        {
            //get the current user id
            int userId = Int32.Parse(Request.Cookies["userId"]!);
            //ModelState.Remove("User");

            //check if there are no errors
            if (ModelState.IsValid)
            {

                //valid user
                if (userId != 0)
                {
                    patternViewModel.ActivePattern.CreationDate = DateTime.Now;
                    patternViewModel.ActivePattern.OwnerId = userId;
                    patternViewModel.ActivePattern.Owner = _crochetDbContext.Users.FirstOrDefault(o => o.UserId == userId);

                    UploadImages(patternViewModel);

                    //create a new tag for the pattern
                    if (!string.IsNullOrEmpty(patternViewModel.Tags))
                    {
                        //should split the tagname if there are more than one i.e tag1, tag2, tag3
                        var splitTags = patternViewModel.Tags.Split(", ").Select(t => t.Trim());

                        foreach (var tag in splitTags)
                        {
                            //check if it exists
                            var existingTag = _crochetDbContext.Tags.FirstOrDefault(t => t.TagName == tag);

                            // Tag doesn't exist, create a new one                       
                            if (existingTag == null)
                            {
                                existingTag = new Tag { TagName = tag };
                                _crochetDbContext.Tags.Add(existingTag);

                                patternViewModel.ActivePattern.PatternTags.Add(new PatternTag { PatternId = patternViewModel.ActivePattern.PatternId, Tag = existingTag });
                            }
                            else
                            {
                                // Tag already exists, associate with the pattern
                                patternViewModel.ActivePattern.PatternTags.Add(new PatternTag { PatternId = patternViewModel.ActivePattern.PatternId, Tag = existingTag });
                            }
                        }
                    }

                    //add the pattern
                    _crochetDbContext.Patterns.Add(patternViewModel.ActivePattern);
                    //save
                    await _crochetDbContext.SaveChangesAsync();

                    TempData["LastActionMessage"] = $"New Pattern \"{patternViewModel.ActivePattern.PatternName}\" was added.";
                    return RedirectToAction("Index", "Pattern");
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
                return View("CreatePattern", patternViewModel);
            }

        }

        public void UploadImages(PatternViewModel viewModel)
        {
            //handle image upload
            //check if there are images in the collection and they are not more than 2
            if (viewModel.Images != null && viewModel.Images.Count > 0 && viewModel.Images.Count <= 2)
            {
                //loop through the collection
                foreach (var image in viewModel.Images)
                {
                    var fileName = Path.GetFileName(image.FileName);

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
                    viewModel.ActivePattern.PatternImages.Add(new PatternImage { Image = newImage });
                }

            }
            else
            {
                TempData["manyImages"] = "You can't upload more than 2 images";
            }
        }

        private int selectedPatternId = 0;
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            selectedPatternId = id;

            var patternViewModel = await _crochetDbContext.Patterns
                .Where(p => p.PatternId == id)
                .Include(p => p.PatternImages).ThenInclude(pi => pi.Image)
                .Include(p => p.PatternComments)
                .Include(p => p.PatternTags).ThenInclude(pt => pt.Tag)
                .FirstOrDefaultAsync();

            if (patternViewModel == null)
            {
                return NotFound();
            }

            // Check if the Owner is null
            if (patternViewModel.Owner == null)
            {
                // Fetch the owner details separately
                var owner = await _crochetDbContext.Users
                    .Where(u => u.UserId == patternViewModel.OwnerId)
                    .FirstOrDefaultAsync();

                // If owner is still null, handle accordingly (e.g., return an error view)
                if (owner == null)
                {
                    return NotFound("Owner not found");
                }

                patternViewModel.Owner = owner;
            }

            //get the tags associated with the pattern
            var tags = _crochetDbContext.PatternTags.Where(pt => pt.PatternId == patternViewModel.PatternId).Select(pt => pt.Tag.TagName).ToList();

            //join them
            var joinedTags = string.Join(", ", tags.Select(t => t.Trim()));

            // Continue building the ViewModel
            var patternDetailViewModel = new PatternDetailViewModel
            {
                ActivePattern = patternViewModel,
                TagNames = joinedTags,
                Images = patternViewModel.PatternImages
                    .Where(pi => pi.Image != null)
                    .Select(pi => pi.Image.ImageSrc)
                    .ToList(),
                Owner = patternViewModel.Owner.FirstName + " " + patternViewModel.Owner.LastName
            };

            // Get recommended patterns
            List<PatternSummaryViewModel> recommendedPatterns = GetRecommendedPatterns();
            ViewBag.RecommendedPatterns = recommendedPatterns;

            return View("PatternDetails", patternDetailViewModel);
        }

        public List<PatternSummaryViewModel> GetRecommendedPatterns()
        {
            List<PatternSummaryViewModel> recommendedpatterns = _crochetDbContext.Patterns.Include(p => p.PatternImages)
                .Include(p => p.PatternComments)
                .Include(p => p.Owner)
                .OrderBy(p => p.PatternName)
                .Select(p => new PatternSummaryViewModel()
                {
                    ActivePattern = p,
                    NumberOfComments = p.PatternComments.Count(),
                    Images = p.PatternImages.Select(pi => pi.Image.ImageSrc).ToList()
                }).OrderBy(g => Guid.NewGuid()).Where(p => p.ActivePattern.PatternId != selectedPatternId).Take(4).ToList();
            return recommendedpatterns;
        }


        [HttpGet]
        public async Task<IActionResult> EditPattern(int id)
        {
            //find the pattern with the id
            var existingPattern = await _crochetDbContext.Patterns
                .Include(p => p.PatternTags).ThenInclude(pt => pt.Tag)
                .Include(p => p.PatternImages)
                .FirstOrDefaultAsync(p => p.PatternId == id);

            //if the pattern exists
            if (existingPattern != null)
            {
                //get the tags associated with the pattern
                var tags = existingPattern.PatternTags.Select(pt => pt.Tag.TagName).ToList();

                //join the tags into a string
                var joinedTags = string.Join(", ", tags.Select(t => t.Trim()));

                PatternViewModel viewModel = new PatternViewModel()
                {
                    ActivePattern = existingPattern,
                    Tags = joinedTags
                };

                return View(viewModel);
            }
            else return NotFound();

        }

        public void UpdateImages(PatternViewModel viewModel)
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
                        viewModel.ActivePattern.PatternImages.Add(new PatternImage { Image = newImage });
                    }
                }


            }
            else
            {
                TempData["manyImages"] = "You can't upload more than 2 images";
            }
        }


        [HttpPost]
        public async Task<IActionResult> EditPattern(PatternViewModel patternViewModel)
        {
            if (ModelState.IsValid)
            {

                int userId = Int32.Parse(Request.Cookies["userId"]!);

                var user = _crochetDbContext.Users.Find(userId);

                patternViewModel.ActivePattern.OwnerId = userId;
                patternViewModel.ActivePattern.Owner = user;
                patternViewModel.ActivePattern.PatternTags.Clear();


                UpdateImages(patternViewModel);

                //first split the string of incoming tags
                var splitTags = patternViewModel.Tags.Split(", ").Select(t => t.Trim());

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
                            existingTag = new Tag { TagName = tagName };
                            _crochetDbContext.Tags.Add(existingTag);

                            patternViewModel.ActivePattern.PatternTags.Add(new PatternTag { PatternId = patternViewModel.ActivePattern.PatternId, Tag = existingTag });
                        }
                    }
                }

                // Save changes to the database
                _crochetDbContext.Patterns.Update(patternViewModel.ActivePattern);
                await _crochetDbContext.SaveChangesAsync();
                return RedirectToAction("Details", new { id = patternViewModel.ActivePattern.PatternId });
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
            return View(patternViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeletePattern(int id, PatternViewModel patternViewModel)
        {
            // Find the pattern
            var pattern = await _crochetDbContext.Patterns
                    .Include(p => p.PatternTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.PatternImages)
                    .FirstOrDefaultAsync(p => p.PatternId == id);

            if (pattern == null)
            {
                return NotFound();
            }

            patternViewModel = new PatternViewModel()
            {
                ActivePattern = pattern
            };
            return View(patternViewModel);
        }



        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Find the pattern
            var pattern = await _crochetDbContext.Patterns
                    .Include(p => p.PatternTags).ThenInclude(pt => pt.Tag)
                    .Include(p => p.PatternImages)
                    .FirstOrDefaultAsync(p => p.PatternId == id);

            if (pattern == null)
            {
                return NotFound();
            }

            //find tags associated with the pattern and so on then remove them.
            var patternTags = _crochetDbContext.PatternTags
                    .Include(pt => pt.Tag)
                    .Where(pt => pt.PatternId == id);

            _crochetDbContext.PatternTags.RemoveRange(patternTags);


            //remove patternimages and images
            var patternImages = _crochetDbContext.PatternImage
                    .Include(p => p.Image)
                    .Where(p => p.PatternId == id);

            foreach (var image in patternImages)
            {
                //find the image
                var Image = _crochetDbContext.Images
                   .Find(image.ImageId);

                //delete the image from the folder
                var imageName = Image.ImageSrc;
                var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "patterns");
                var filePath = Path.Combine(directoryPath, imageName);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }

                _crochetDbContext.PatternImage.Remove(image);
                _crochetDbContext.Images.Remove(Image);
            }

            //remove patterncomments and comments
            var patternComments = _crochetDbContext.PatternComments
                    .Include(p => p.Comment)
                    .Where(p => p.PatternId == id);

            _crochetDbContext.PatternComments.RemoveRange(patternComments);

            //Remove user projects
            var userPatterns = _crochetDbContext.UserPatterns
                              .Where(up => up.PatternId == id)
                              .ToList();

            // Delete user projects
            _crochetDbContext.UserPatterns.RemoveRange(userPatterns);

            // Retrieve project patterns associated with the project
            var projectPatterns = _crochetDbContext.ProjectPatterns
                .Where(pp => pp.PatternId == id)
                .ToList();

            // Delete project patterns
            _crochetDbContext.ProjectPatterns.RemoveRange(projectPatterns);

            // Remove the pattern lastly
            _crochetDbContext.Patterns.Remove(pattern);
            await _crochetDbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private CrochetAppDbContext _crochetDbContext;

    }
}
