using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Setup.Areas.Identity.Data;
using Setup.Models;
using System.Security.Claims;

namespace Setup.Controllers
{

    [Authorize]
    public class GameController : Controller
    {
        private readonly AuthContext _context;
        private readonly UserManager<AuthUser> _userManager;
        public GameController(AuthContext context, UserManager<AuthUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        Game _game = new Game();
        public IActionResult Index()
        {
            var userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
           var user = _context.authUsers.Where(u => u.Email == userEmail).FirstOrDefault();
            _game.PlayerId = user.Id;
            Console.WriteLine(userEmail  );
            Console.WriteLine(user.Id);
            return View(_game);
        }


        [Route("api/[controller]")]
        // PUT: api/Game/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutGame(Game game)
        {

            if (_context.authUsers.Where(f=> f.Id == game.PlayerId).FirstOrDefaultAsync().Result == null)
            {
                return NotFound();
            }
            if (_context.authUsers.Where(f => f.highScore <= game.score && f.Id == game.PlayerId).Any())
            {
                var user =_context.authUsers.Where(f => f.Id == game.PlayerId).FirstOrDefaultAsync().Result;
                user.highScore = game.score;
                _context.authUsers.Update(user);
            }
            else
            {
             
               

                return Ok("Score is lager dan highscore");
            }
             try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }


    }
}
