using Microsoft.AspNetCore.Mvc;
using Setup.Models;

namespace Setup.Controllers
{
    public class GameController : Controller
    {
        Game game = new Game();
        public IActionResult Index()
        {   
            return View(game);
        }

    }
}
