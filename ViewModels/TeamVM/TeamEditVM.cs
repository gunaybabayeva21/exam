using Lumia.Models;

namespace Lumia.ViewModels.TeamVM
{
    public class TeamEditVM
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int JobId { get; set; }
        public IFormFile Image { get; set; } = null!;
        public List<Job>? Jobs { get; set; }
    }
}
