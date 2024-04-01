using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CroKnitters.Controllers
{
    public class CommentController : Controller
    {
        public CommentController(CrochetAppDbContext crochetDbContext)
        {
            _crochetDbContext = crochetDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewProjectComments(int id)
        {
            patproj = 1;
            TempData["Trial"] = patproj;
            TempData["Trial2"] = id;

            List<ProjectComment> projectComments = _crochetDbContext.ProjectComments.Include(p => p.Comment).Where(p => p.ProjectId == id).ToList();

            List<Comment> totalComments = _crochetDbContext.Comments.Include(p => p.Owner).ToList();

            List<Comment> comments = new List<Comment>();

            foreach (var comment in totalComments)
            {
                foreach (var project in projectComments)
                {
                    if (project.CommentId == comment.CommentId)
                    {
                        comments.Add(comment);
                    }
                }
            }

            return View("Index", comments);
        }

        public IActionResult ViewPatternComments(int id)
        {
            patproj = 2;
            TempData["Trial"] = patproj;
            TempData["Trial2"] = id;

            List<PatternComment> patternComments = _crochetDbContext.PatternComments.Include(p => p.Comment).Where(p => p.PatternId == id).ToList();

            List<Comment> totalComments = _crochetDbContext.Comments.Include(p => p.Owner).ToList();

            List<Comment> comments = new List<Comment>();

            foreach (var comment in totalComments)
            {
                foreach (var pattern in patternComments)
                {
                    if (pattern.CommentId == comment.CommentId)
                    {
                        comments.Add(comment);
                    }
                }
            }

            return View("Index", comments);
        }

        [HttpGet]
        public IActionResult AddComment()
        {
            return View("AddComment");
        }

        //accept the request containing all the relevant details
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentViewModel commentViewModel)
        {
            patproj = Convert.ToInt32(TempData["Trial"]);
            CommentID = Convert.ToInt32(TempData["Trial2"]);

            //get the current user id
            int userId = Int32.Parse(Request.Cookies["userId"]!);

            //check if there are no errors
            if (ModelState.IsValid)
            {

                //valid comment
                if (userId != 0)
                {
                    commentViewModel.ActiveComment.CreationDate = DateTime.Now;
                    commentViewModel.ActiveComment.OwnerId = userId;
                    commentViewModel.ActiveComment.Likes = 0;
                    commentViewModel.ActiveComment.Owner = _crochetDbContext.Users.FirstOrDefault(o => o.UserId == userId);
                    commentViewModel.ActiveComment.Admin = _crochetDbContext.Admin.FirstOrDefault(o => o.AdminId == 1);


                    if (patproj == 1)
                    {
                        int proId = CommentID;

                        Project? ProCom = _crochetDbContext.Projects.FirstOrDefault(p => p.ProjectId == proId);

                        commentViewModel.ActiveComment.ProjectComments.Add(new ProjectComment { Project = ProCom });

                    }
                    else
                    {
                        int patId = CommentID;

                        Pattern? PatCom = _crochetDbContext.Patterns.FirstOrDefault(p => p.PatternId == patId);

                        commentViewModel.ActiveComment.PatternComments.Add(new PatternComment { Pattern = PatCom });
                    }

                    //add the comment
                    _crochetDbContext.Comments.Add(commentViewModel.ActiveComment);
                    //save
                    await _crochetDbContext.SaveChangesAsync();

                    return RedirectToAction("Index", "Home");
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
                return View("AddComment", commentViewModel);
            }

        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            patproj = Convert.ToInt32(TempData["Trial"]);

            // Find the comment
            var Comment = await _crochetDbContext.Comments
                   .Include(p => p.ProjectComments).Include(p => p.PatternComments)
                   .FirstOrDefaultAsync(p => p.CommentId == id);

            if (Comment == null)
            {
                return NotFound();
            }

            if (patproj == 1)
            {
                _crochetDbContext.ProjectComments.RemoveRange(Comment.ProjectComments);

            }
            else
            {
                _crochetDbContext.PatternComments.RemoveRange(Comment.PatternComments);
            }


            // Remove the comment
            _crochetDbContext.Comments.Remove(Comment);
            await _crochetDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> AdminDeleteComment(int id)
        {
            // Find the comment
            var Comment = await _crochetDbContext.Comments
                   .Include(p => p.ProjectComments).Include(p => p.PatternComments)
                   .FirstOrDefaultAsync(p => p.CommentId == id);

            if (Comment == null)
            {
                return NotFound();
            }


            _crochetDbContext.ProjectComments.RemoveRange(Comment.ProjectComments);
            _crochetDbContext.PatternComments.RemoveRange(Comment.PatternComments);


            // Remove the comment
            _crochetDbContext.Comments.Remove(Comment);
            await _crochetDbContext.SaveChangesAsync();

            return Ok();

        }

        [HttpGet]
        public async Task<IActionResult> EditComment(int id)
        {
            //find the comment with the id
            var existingComment = await _crochetDbContext.Comments.Include(p => p.ProjectComments).Include(p => p.PatternComments)
                .FirstOrDefaultAsync(p => p.CommentId == id);

            //if the comment exists
            if (existingComment != null)
            {
                CommentViewModel viewModel = new CommentViewModel()
                {
                    ActiveComment = existingComment
                };

                Console.WriteLine($"Error: {viewModel.ActiveComment.ProjectComments.Count}");

                return View(viewModel);
            }
            else return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel CommentViewModel)
        {
            Console.WriteLine($"Error: {CommentViewModel.ActiveComment.CommentId}");


            if (ModelState.IsValid && (CommentViewModel.ActiveComment.CommentId != 0))
            {
                int userId = Int32.Parse(Request.Cookies["userId"]!);

                CommentViewModel.ActiveComment.OwnerId = userId;
                CommentViewModel.ActiveComment.Owner = _crochetDbContext.Users.FirstOrDefault(o => o.UserId == userId);
                CommentViewModel.ActiveComment.Admin = _crochetDbContext.Admin.FirstOrDefault(o => o.AdminId == CommentViewModel.ActiveComment.AdminId);


                _crochetDbContext.Comments.Update(CommentViewModel.ActiveComment);

                // Save changes to the database
                await _crochetDbContext.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
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
                return View("EditComment", CommentViewModel);
            }

        }

        public async Task<IActionResult> LikeComment(int id)
        {
            //find the comment with the id
            var existingComment = await _crochetDbContext.Comments.Include(p => p.ProjectComments).Include(p => p.PatternComments)
                .FirstOrDefaultAsync(p => p.CommentId == id);

            int likes = 0;
            likes = existingComment.Likes;
            existingComment.Likes = likes + 1;

            _crochetDbContext.Comments.Update(existingComment);

            // Save changes to the database
            await _crochetDbContext.SaveChangesAsync();
            return RedirectToAction("Index", "Home");


        }

        private CrochetAppDbContext _crochetDbContext;

        public int patproj = 0;
        public int CommentID = 0;

    }
}
