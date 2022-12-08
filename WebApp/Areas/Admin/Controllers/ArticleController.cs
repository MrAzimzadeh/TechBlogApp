using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles
                .Include(x=>x.User)
                .Include(x=>x.Category)
                .Include(x=>x.ArticleTags)
                .ThenInclude(x=>x.Tag)
                .Where(data=>data.IsDeleted == false).ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            return View();
        }


        public IActionResult Edit(int id)
        {
            return View();
        }
    }
}
