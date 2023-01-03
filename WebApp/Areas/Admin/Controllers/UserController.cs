using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Areas.Admin.ViewModels;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor contextAccessor, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _contextAccessor = contextAccessor;
            _env = env;
        }
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddRole(string id)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (id == null) return NotFound();

            var userRoles = (await _userManager.GetRolesAsync(user)).ToList();
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            UserRoleAddViewModel vm = new()
            {
                User = user,
                Roles = roles.Except(userRoles)
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddRole(string id, string role)
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (id == null) return NotFound();


            var userAddRole = await _userManager.AddToRoleAsync(user, role);
            if (!userAddRole.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            User user = await _userManager.FindByIdAsync(userId);
            return View(user);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> UserInfo(User user, IFormFile Photo)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var findUser = await _userManager.FindByEmailAsync(userId);
            var photoUrl = "";
            if (Photo == null)
            {
                photoUrl = "/uploads/avatar.png";
            }
            else
            {
                photoUrl = ImageHelper.UploadSinglePhoto(Photo, _env);

            }


            try
            {
                findUser.PhotoUrl = photoUrl;
                findUser.Name = user.Name;

                findUser.Surname= user.Surname;
                findUser.AboutAuthor= user.AboutAuthor;
                await _userManager.UpdateAsync(user);

            }
            catch (Exception)
            {

                return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
