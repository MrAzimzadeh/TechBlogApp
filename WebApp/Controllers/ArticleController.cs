using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;

        public ArticleController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Deteil(int? id, string seourl)
        {
            if (id.Value == null)
            {
                return NotFound();
            }
            var article = _context.Articles
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Id == id.Value);
            if (article == null)
            {
                return NotFound();
            }
            DeteilVM deteilVM = new()
            {
                Article = article
            };
            return View(deteilVM);
        }

    }
}
