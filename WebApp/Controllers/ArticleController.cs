using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly UserManager<User> _userManager;

        public ArticleController(AppDbContext context, IHttpContextAccessor contextAccessor, UserManager<User> userManager)
        {
            _context = context;
            _contextAccessor = contextAccessor;
            _userManager = userManager;
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
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .FirstOrDefault(x => x.Id == id.Value);
            if (article == null)
            {
                return NotFound();
            }

            var cookie = _contextAccessor.HttpContext.Request.Cookies[$"Views"];

            string[] findCookie = { "" };
            if (cookie != null)
            {
                findCookie = cookie.Split("-").ToArray();
            }

            if (!findCookie.Contains(article.Id.ToString()))
            {
                Response.Cookies.Append($"Views", $"{cookie} - {article.Id}",
                    new CookieOptions
                    {
                        Secure = true,
                        HttpOnly = true,
                        Expires = DateTime.Now.AddMinutes(1)
                    }
                    );


                article.Views += 1;
                _context.Articles.Update(article);
                _context.SaveChanges();
            }


            var suggestArticle = _context.Articles.Include(x => x.Category).Where(x => x.Id != article.Id && x.CategoryId == article.CategoryId).Take(2).ToList();

            var after = _context.Articles
                .FirstOrDefault(x => x.Id > id);
            var before = _context.Articles
                .FirstOrDefault(X => X.Id < id);
            DeteilVM deteilVM = new()
            {
                Article = article,
                Suggestions = suggestArticle,
                After = after,
                Befero = before,

            };
            return View(deteilVM);

        }



        [HttpPost]
        public async Task<IActionResult> Deteil(Comment comment)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Comment newComment = new()
            {
                CommentedDate = DateTime.Now,
                UserId = userId,
                ArticleId = comment.ArticleId,
                Content = comment.Content
            };

            var article = _context.Articles.FirstOrDefault(x => x.Id == comment.ArticleId);
            await _context.Comments.AddAsync(newComment);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Deteil), new { id = article.Id, article.SeoUrl });
        }
    }
}
