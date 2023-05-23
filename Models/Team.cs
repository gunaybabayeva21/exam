namespace Lumia.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Surname { get; set; }=null!;
        public string Description { get; set; } = null!;
        public string ImageName { get; set; } = null!;
        public Job Job { get; set; }=null!;
        public int JobId { get; set; }
    }
}
