using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;
using System.Diagnostics;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var category = _context.Categories.ToList();
            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            var findCatageory = _context.Categories.FirstOrDefault(x => x.Name == category.Name);
            if (findCatageory != null)
            {
                ViewBag.CategoryExist = "This category is exist";
                return View(findCatageory);
            }
            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));



        }
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            var findCatageory = _context.Categories.FirstOrDefault(x => x.Name == category.Name);
            if (findCatageory != null)
            {
                ViewBag.CategoryExist = "This category is exist";
                return View(findCatageory);
            }

            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            return View();
        
        }
        [HttpPost]
        public IActionResult Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges(); 
            return RedirectToAction(nameof(Index));
        }
    }
}
