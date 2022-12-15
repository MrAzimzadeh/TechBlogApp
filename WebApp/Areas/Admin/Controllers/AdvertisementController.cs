using Microsoft.AspNetCore.Mvc;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdvertisementController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public AdvertisementController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {

            var ads = _context.Advertisements.ToList();
            return View(ads);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Advertisement advertisement, IFormFile Photo)
        {

            var path = "/uploads/" + Guid.NewGuid() + Photo.FileName;
            using (var fileStream = new FileStream(_env.WebRootPath + path, FileMode.Create))
            {
                Photo.CopyTo(fileStream);
            }
            Advertisement ads = new()
            {
                Name = advertisement.Name,
                PhotoUrl = path,
                Price = advertisement.Price,
                Rate = advertisement.Rate,
                SizeX = advertisement.SizeX,
                SizeY = advertisement.SizeY,
                DirectionAddress = "https://" + advertisement.DirectionAddress,
                CreatedDate = DateTime.Now, //aftomatik olur deye 
                Click = 0,
                View = 0

            };
            _context.Advertisements.Add(ads);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Deteil(int id)
        {
            var deteil = _context.Advertisements.FirstOrDefault(x => x.Id == id);
            return View(deteil);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _context.Advertisements.SingleOrDefault(a => a.Id == id);

           
            return View(data);
        }


    }
}
