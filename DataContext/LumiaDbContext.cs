using Lumia.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lumia.DataContext
{
    public class LumiaDbContext :IdentityDbContext<AppUser>
    {
        public LumiaDbContext(DbContextOptions options) : base(options)
        { 


        }
       public DbSet<Team> Teams { get; set; }
       public   DbSet<Job> Jobs { get; set; }


    }
}
