using Microsoft.AspNetCore.Mvc;
using Setup.Models;

namespace Setup.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index(Developer developer)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel(developer);
            return View(profileViewModel);
        }
    }
}
