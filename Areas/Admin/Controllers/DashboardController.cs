using Lumia.DataContext;
using Microsoft.AspNetCore.Mvc;

namespace Lumia.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashBoardController:Controller
    {
     

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
