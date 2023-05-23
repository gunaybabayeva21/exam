using System.ComponentModel.DataAnnotations;

namespace Lumia.Models
{
    public class Job
    {     
        public Job() 
        { 
            Teams= new List<Team>();    
        }

           

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
