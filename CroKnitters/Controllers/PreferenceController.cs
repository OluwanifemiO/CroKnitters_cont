using CroKnitters.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CroKnitters.Controllers
{
    public class PreferenceController : Controller
    {
        private readonly CrochetAppDbContext _context;

        public PreferenceController(CrochetAppDbContext context)
        {
            _context = context;
        }

        [HttpPost]

        public IActionResult EditPreference(Preference preference)
        {
            int userId = int.Parse(Request.Cookies["userId"]!);

            var p = _context.Preferences.FirstOrDefault(p => p.UserId == userId);
            if (p == null)
            {
                p = new Preference
                {
                    UserId = userId,
                    LanguageId = preference.LanguageId,
                    ThemeId = preference.ThemeId

                };


                _context.SaveChanges();
                TempData["SuccessMessage"] = "Preference updated";
            }
            else
            {
                p.LanguageId = preference.LanguageId;
                p.ThemeId = preference.ThemeId;
                TempData["error"] = "Preference updated";
            }

            _context.SaveChanges();


            return RedirectToAction("EditPreference");
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EditPreference()
        {
            return View();
        }
    }
}