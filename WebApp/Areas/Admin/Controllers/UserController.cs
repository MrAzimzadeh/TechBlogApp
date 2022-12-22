using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Areas.Admin.ViewModels;
using WebApp.Models;

namespace WebApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var users = _userManager.Users.ToList();
            return View(users);
        }

        public async Task<IActionResult> AddRole(string id) 
        {
            if (id == null) return NotFound();
            User user = await _userManager.FindByIdAsync(id);
            if (id == null) return NotFound();

            var userRoles =( await _userManager.GetRolesAsync(user)).ToList();
            var roles = _roleManager.Roles.Select(x => x.Name).ToList();

            UserRoleAddViewModel vm = new()
            {
                User = user,
                Roles = roles.Except(userRoles)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddRole(string id , string role
            )
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
    }
}
