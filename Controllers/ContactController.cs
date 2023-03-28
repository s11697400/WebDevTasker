using Microsoft.AspNetCore.Mvc;
using Setup.Models;

namespace Setup.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index(Developer developer)
        {
            ProfileViewModel profileViewModel = new ProfileViewModel(developer);
            return View(profileViewModel);
        }

        [HttpPost] 
        public IActionResult Result(string Naam, int Id)
        {
            Console.WriteLine("RESULT CONTROLLER");
            Console.WriteLine(Naam);
            Console.WriteLine(Id.ToString());
            Person p = new Person(Naam, Id, new List<Friendship>());
            Console.WriteLine(p.Username);
            PersonViewModel personViewModel = new PersonViewModel(p);
            Console.WriteLine(personViewModel.Name);
            return View("ResultPage", personViewModel);
        }

        public IActionResult ResultPage(PersonViewModel pvw)
        {


            return View(pvw);
        }
    }
}
