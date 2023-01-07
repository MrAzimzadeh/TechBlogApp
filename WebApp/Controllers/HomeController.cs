using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
;

            var articles = _context.Articles
            .Include(x => x.Category)
            .Include(x=>x.User)
            .Where(x => x.IsDeleted == false || x.IsActive == true)
            .ToList();


            ViewData["MaxValue"] =  _context.Articles.OrderBy(x=>x.Views).ToList().TakeLast(2);
            

            HomeVM homeVM= new()
            {
                Articles= articles,
            };

            
            return View(homeVM);

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}