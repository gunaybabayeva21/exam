using Lumia.DataContext;
using Lumia.Models;
using Lumia.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Lumia.Controllers
{
    public class HomeController : Controller
    {
        private readonly LumiaDbContext _lumiaDbContext;
        public HomeController(LumiaDbContext lumiaDbContext)
        {
               _lumiaDbContext= lumiaDbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Team> teams= await _lumiaDbContext.Teams.Include(c=>c.Job).ToListAsync();
            HomeVM homeVM = new HomeVM()
            {
                Teams= teams
            };
            return View(homeVM);
        }
        
    }
}