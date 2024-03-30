using CroKnitters.Entities;
using CroKnitters.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CroKnitters.Controllers
{

    public class ManagePatternsController : Controller
    {
        private CrochetAppDbContext _dbContext;

        public ManagePatternsController(CrochetAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            if (Request.Cookies.TryGetValue("role", out string role) && role == "admin")
            {
                if (Request.Cookies.TryGetValue("adminId", out string adminId))
                {
                    // Both "role" and "adminId" cookies exist
                    var feed = new Feed();


                    var patternsQuery = _dbContext.Patterns.AsQueryable();

                    var projectsQuery = _dbContext.Projects.AsQueryable();


                    var patterns = patternsQuery
                        .Include(p => p.PatternImages)
                        .Include(p => p.PatternComments)
                        .Include(p => p.Owner)
                        .Select(p => new PatternSummaryViewModel
                        {
                            ActivePattern = p,
                            NumberOfComments = p.PatternComments.Count,
                            Images = p.PatternImages.Select(img => img.Image.ImageSrc).ToList() // Assuming ImageSrc is the path to the image
                        }).ToList();

                    var projects = projectsQuery
                        .Include(p => p.ProjectImages)
                        .Include(p => p.ProjectComments)
                        .Select(p => new ProjectSummaryViewModel
                        {
                            ActiveProject = p,
                            NumberOfComments = p.ProjectComments.Count,
                            Images = p.ProjectImages.Select(img => img.Image.ImageSrc).ToList() // Assuming ImageSrc is the path to the image
                        }).ToList();

                    feed.Patterns = patterns;
                    feed.Projects = projects;
                    return View("Index", feed);
                }
            }

            // If any of the cookies are missing or the role is not "admin", redirect to login
            return RedirectToAction("Login", "Account");
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAdminPosts(int id)
        {
            Pattern @pattern = _dbContext.Patterns.Find(id);

            if (@pattern == null)
            {
                return HttpNotFound();
            }

            return View(@pattern);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pattern @pattern = _dbContext.Patterns.Find(id);
            _dbContext.Patterns.Remove(@pattern);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}