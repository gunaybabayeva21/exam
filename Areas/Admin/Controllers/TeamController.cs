using Lumia.DataContext;
using Lumia.Models;
using Lumia.ViewModels.TeamVM;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System.Drawing;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamController : Controller
    {
        private readonly LumiaDbContext _context;
        private readonly IWebHostEnvironment _ebHostEnvironment;
        public TeamController(LumiaDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _ebHostEnvironment= webHostEnvironment;
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            List<Team> team = await _context.Teams.ToListAsync();
            return View(team);
        }
        public async Task<IActionResult> Create()
        {
            TeamCreateVM teamCreateVM = new TeamCreateVM()
            {
                Jobs = await _context.Jobs.ToListAsync(),

            };
            return View(teamCreateVM);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TeamCreateVM newteam)
        {
            if (!ModelState.IsValid)
            {
                newteam.Jobs = await _context.Jobs.ToListAsync();
                return View(newteam);
            }
            Team team = new Team()
            {
                Name = newteam.Name,
                Surname = newteam.Surname,
                Description = newteam.Description,
                JobId = newteam.JobId
            };
           if(newteam.Image.ContentType.Contains ("image/")&& newteam.Image.Length/1024>2048 )
           { 
                newteam.Jobs=await _context.Jobs.ToListAsync();
                ModelState.AddModelError("Image", "Error");
           }

            string newFileName = Guid.NewGuid().ToString() + newteam.Image.FileName;
            string path = Path.Combine(_ebHostEnvironment.WebRootPath, "assets", "img", "testimonials", newFileName);
            using (FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                newteam.Image.CopyToAsync(stream);
            }
           team.ImageName=newFileName; 
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Edit(int id)
        {
            Team? team = await _context.Teams.FindAsync(id);
            if (team == null) { return NotFound(); }
            TeamEditVM teamEditVM = new TeamEditVM()
            {
                Name = team.Name,
                Jobs = await _context.Jobs.ToListAsync(),
                Surname = team.Surname,
                Description = team.Description,
                JobId = team.JobId,
            };
            return View(teamEditVM);
        }
        [HttpPost]
        public async Task<IActionResult>Edit(int id, TeamEditVM newteam)
        {
            if (!ModelState.IsValid)
            {
                newteam.Jobs = await _context.Jobs.ToListAsync();
                return View(newteam);
            }
            Team? team = await _context.Teams.FindAsync(id);
            if (team == null) { return NotFound(); }
            if (newteam.Image == null!)
            {
                string path = Path.Combine(_ebHostEnvironment.WebRootPath, "assets", "img", "testimonials", team.ImageName);
                if(System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string newFileName = Guid.NewGuid().ToString() + newteam.Image.FileName;
                using(FileStream stream = new FileStream(path, FileMode.Create))
                {
                    newteam.Image.CopyTo(stream);
                }
                team.ImageName = newFileName;

            }
            team.JobId = newteam.JobId;
            team.Surname = newteam.Surname;
            team.Description = newteam.Description;
            team.Name = newteam.Name;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }
            string path = Path.Combine(_ebHostEnvironment.WebRootPath, "assets", "img", "testimonials", team.ImageName);
            {
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
            }
            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
