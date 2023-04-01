using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Setup.Areas.Identity.Data;
using Setup.Models;
using System.Diagnostics;
using System.Security.Claims;
using System.Security.Principal;

namespace Setup.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthContext _context;

        private const string PageViews = "PageViews";

        public HomeController(ILogger<HomeController> logger, AuthContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public IActionResult Index()
        {
            UpdatePageViewCookie();
            if (Request.Cookies[PageViews] != null)
            {
                ViewBag.AmountOfViews = Request.Cookies[PageViews].ToString();
            }
            else
            {
                ViewBag.AmountOfViews = 0.ToString();
            }
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void UpdatePageViewCookie()
        {
            var currentCookieValue = Request.Cookies[PageViews];

            if (currentCookieValue == null)
            {
                Response.Cookies.Append(PageViews, "1");
            }
            else
            {
                var newCookieValue = short.Parse(currentCookieValue) + 1;

                Response.Cookies.Append(PageViews, newCookieValue.ToString());

            }
        }
        [Authorize]
        public IActionResult Friends()
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                AuthUser user2 = _context.Users.Where(c => c.UserName == HttpContext.User.Identity.Name).FirstOrDefault();



                //First get user claims    
                /*     var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

                     //Filter specific claim    
                     Console.WriteLine( claims?.FirstOrDefault(x => x.Type.Equals("UserName", StringComparison.OrdinalIgnoreCase))?.Value );*/
                ViewBag.id = user2.Id;
            }
            return View();
        }
      
    }
}