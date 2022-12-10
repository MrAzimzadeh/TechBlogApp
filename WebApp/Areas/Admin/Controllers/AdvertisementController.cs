using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly AppDbContext _context;
        public AdvertisementController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var advertisement = _context.Advertisements.ToList();
            return View(advertisement);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Advertisement advertisement)
        {
            var findAdvertisement = _context.Advertisements.FirstOrDefault(x => x.Name == advertisement.Name);
            if (findAdvertisement != null)
            {
                ViewBag.AdvertisementExist = "This category is exist";
                return View(findAdvertisement);
            }
            _context.Advertisements.Add(advertisement);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));



        }

        public IActionResult Edit(int id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(x => x.Id == id);
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Advertisement advertisement)
        {
            var findAdvertisement = _context.Advertisements.FirstOrDefault(x => x.Name == advertisement.Name);
            if (findAdvertisement != null)
            {
                ViewBag.AdvertisementExist = "This category is exist";
                return View(findAdvertisement);
            }

            _context.Advertisements.Update(advertisement);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            var advertisement = _context.Advertisements.FirstOrDefault(x => x.Id == id);
            return View();

        }
        [HttpPost]
        public IActionResult Delete(Advertisement advertisement)
        {
            _context.Advertisements.Remove(advertisement);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
