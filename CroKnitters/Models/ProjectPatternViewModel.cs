using CroKnitters.Entities;

namespace CroKnitters.Models
{
    public class ProjectPatternViewModel
    {
        public int ActiveProjectId {  get; set; }

        public List<Pattern>? allPatterns { get; set; } = null;

        public int associatedPatternId {  get; set; } 
    }
}
