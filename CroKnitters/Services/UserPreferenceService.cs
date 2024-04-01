using System.Linq;
using CroKnitters.Entities;

namespace CroKnitters.Services
{
    public class UserPreferenceService : IUserPreferenceService
    {
        private readonly CrochetAppDbContext _context;

        public UserPreferenceService(CrochetAppDbContext context)
        {
            _context = context;
        }

        public Preference GetUserPreference(int userId)
        {
            return _context.Preferences.FirstOrDefault(p => p.UserId == userId);
        }

        public void UpdateUserPreference(int userId, int languageId, int themeId)
        {
            var preference = _context.Preferences.FirstOrDefault(p => p.UserId == userId);

            if (preference == null)
            {
                preference = new Preference
                {
                    UserId = userId,
                    LanguageId = languageId,
                    ThemeId = themeId
                };
                _context.Preferences.Add(preference);
            }
            else
            {
                preference.LanguageId = languageId;
                preference.ThemeId = themeId;
            }

            _context.SaveChanges();
        }
    }
}