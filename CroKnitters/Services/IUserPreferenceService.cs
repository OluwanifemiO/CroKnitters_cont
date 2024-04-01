using CroKnitters.Entities;
using System.Linq;

namespace CroKnitters.Services
{
    public interface IUserPreferenceService
    {
        Preference GetUserPreference(int userId);
        void UpdateUserPreference(int userId, int languageId, int themeId);
    }
}