using WebApp.Models;

namespace WebApp.Areas.Admin.ViewModels
{
    public class UserRoleAddViewModel
    {
        public User User { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
