using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using WebApp.Helpers;
using System.Diagnostics;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IWebHostEnvironment _env;

        public ArticleController(AppDbContext context, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _env = env;
        }

        public IActionResult Index()
        {
            var articles = _context.Articles
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.ArticleTags)
                .ThenInclude(x => x.Tag)
                .Where(data => data.IsDeleted == false).ToList();
            return View(articles);
        }

        [HttpGet]
        public IActionResult Create()
        {

            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewData["Tags"] = tags;
            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Article article, List<int> Tags, IFormFile Photo)
        {

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            article.UserId = userId;
            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewData["Tags"] = tags;
            var photo = ImageHelper.UploadSinglePhoto(Photo, _env);
            var seo_url = SeoUrlHelper.SeoUrl(article.Title);
            article.UserId = userId;
            article.CreatedDate = DateTime.Now;
            article.UpdatedDate = DateTime.Now;
            article.Views = 0;
            article.PhotoUrl = photo;
            article.SeoUrl = seo_url;


            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
            List<ArticleTag> tagList = new();
            for (int i = 0; i < Tags.Count; i++)
            {
                ArticleTag articleTag = new()
                {
                    ArticleId = article.Id,
                    TagId = Tags[i]
                };
                tagList.Add(articleTag);

            }
            await _context.ArticleTags.AddRangeAsync(tagList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Edit(int id)
        {
            var categories = _context.Categories.ToList();
            var tags = _context.Tags.ToList();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewData["Tags"] = tags;
            var article = _context.Articles.Include(x => x.ArticleTags).FirstOrDefault(x => x.Id == id);



            return View(article);
        }
        [HttpPost]
        public IActionResult Edit(Article article, IFormFile Photo, List<int> Tags)
        {

            article.UpdatedDate = DateTime.Now;
            article.IsActive = false;
            article.SeoUrl = SeoUrlHelper.SeoUrl(article.Title);
            if (Photo != null)
                article.PhotoUrl = ImageHelper.UploadSinglePhoto(Photo, _env);


            var oldTags = _context.ArticleTags.Where(x => x.ArticleId == article.Id).ToList();
            _context.ArticleTags.RemoveRange(oldTags);

            _context.SaveChanges();
            List<ArticleTag> tagList = new();
            for (int i = 0; i < Tags.Count; i++)
            {
                ArticleTag articleTag = new()
                {
                    ArticleId = article.Id,
                    TagId = Tags[i]
                };
                tagList.Add(articleTag);
            }


            _context.ArticleTags.AddRange(tagList);
            _context.Articles.Update(article);

            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }



        public IActionResult Deteil(int id)
        {

            var articles = _context.Articles.FirstOrDefault(a => a.Id == id);
            
            return View(articles);
        
        }
        public IActionResult Delete(int id)
        {
            var data = _context.Articles.SingleOrDefault(a => a.Id == id);

            return View(data);
        }

        [HttpPost]

        public IActionResult Delete(Article  article)
        {
            var result = _context.Articles.FirstOrDefault(x => x.Id == article.Id);
            result.IsDeleted = true;
            _context.Articles.Update(result);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
