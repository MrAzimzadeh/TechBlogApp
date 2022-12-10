using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly AppDbContext _context;
        public TagController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var tag = _context.Tags.ToList();
            return View(tag);
        }

        //
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tag tag)
        {
            var tagFind = _context.Tags.FirstOrDefault(x => x.Name == tag.Name);
            if (tagFind != null)
            {
                ViewBag.TagExist = "This category is exist";
                return View(tagFind);
            }
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
        //

        public IActionResult Edit(int id)
        {
            var tag = _context.Tags.FirstOrDefault(x => x.Id == id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Tag tag)
        {
            var tagFind = _context.Tags.FirstOrDefault(x => x.Name == tag.Name);
            if (tagFind != null)
            {
                ViewBag.TagExist = "This category is exist";
                return View(tagFind);
            }

            _context.Tags.Update(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var tags = _context.Tags.FirstOrDefault(x => x.Id == id);
            return View(tags);

        }
        [HttpPost]
        public IActionResult Delete(Tag tag)
        {
            _context.Tags.Remove(tag);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
