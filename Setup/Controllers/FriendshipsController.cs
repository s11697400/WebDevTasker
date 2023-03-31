using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NuGet.Protocol;
using Setup.Areas.Identity.Data;
using Setup.DataContext;
using Setup.Models;
using Formatting = Newtonsoft.Json.Formatting;

namespace Setup.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FriendshipsController : ControllerBase
    {
        private readonly AuthContext _context;

        public FriendshipsController(AuthContext context)
        {
            _context = context;
        }
        public static void SetPropertyValue<T, TValue>( T target, Expression<Func<T, TValue>> memberLamda, TValue value)
        {
            var memberSelectorExpression = memberLamda.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    property.SetValue(target, value, null);
                }
            }
        }
        private async Task<string> findUserName(string id)
        {
            return _context.authUsers.Where(a => a.Id == id).FirstOrDefaultAsync().Result.UserName;
        }

        // GET: api/Friendships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Friendship>>> GetFriends()
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }

            return await _context.Friends.ToListAsync();
        }

        // GET: api/Friendships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Friendship>>> GetFriendship(string? id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
    
            await _context.Friends.Where(f => f.UserId1 == id).ForEachAsync(async (f) =>
           {
               var user = findUserName(f.UserId1).Result;
               f.UserName1 = user;
               var user2 = findUserName(f.UserId2).Result;

               f.UserName2 = user2;
               Console.WriteLine(user2);

           });
            var friendship = await _context.Friends.Where(f => f.UserId1 == id).ToListAsync();
       
            if (friendship == null)
            {
                return NotFound();
            }


            return friendship;
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(int? id)
        {
            Console.WriteLine("PUTTTTT");
             if(_context.Friends.FindAsync(id).Result == null) 
            {
                return NotFound();
            }
            var originalFriendship = _context.Friends.Where(f => f.FriendshipId == id).FirstOrDefault();
            
            var friendship = _context.Friends.Where(f => (f.UserId2 == originalFriendship.UserId2 && f.UserId1 == originalFriendship.UserId1)).FirstOrDefaultAsync().Result;
            if (friendship == null)
            {
                return NotFound();
            }
            friendship.accepted = true;
            _context.Friends.Update(friendship);
            var user1 = _context.Users.FindAsync(originalFriendship.UserId1).Result;
            var user2 = _context.Users.FindAsync(originalFriendship.UserId2).Result;
            var friendshipNew = new Friendship(user2, user1);
            if (friendshipNew == null)
            {
                return NotFound();
            }
            friendshipNew.accepted = true;
            _context.Friends.Add(friendshipNew);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FriendshipExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Friendships/
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
          /*  Console.WriteLine(friendship.UserId1);
            Console.WriteLine(friendship.UserName2);
            return Ok(friendship.UserName2);*/
            if (_context.Friends == null)
            {
                return Problem("Entity set 'PersonDatabaseContext.Friends'  is null.");
            }
            if(friendship.UserId1== null)
            {
                return NoContent();
            }
            friendship.UserId2 = _context.authUsers.Where(f=> f.UserName == friendship.UserName2).FirstOrDefault().Id;
            _context.Friends.Add(friendship);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FriendshipExists(friendship.FriendshipId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFriendship", new { id = friendship.UserId1 }, friendship);
        }

        // DELETE: api/Friendships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFriendship(int? id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var originalFriendship = _context.Friends.Where(f => f.FriendshipId == id).FirstOrDefaultAsync().Result;
            var friendship = _context.Friends.Where(f => (f.UserId2 == originalFriendship.UserId2 && f.UserId1 == originalFriendship.UserId1)).FirstOrDefaultAsync().Result;
            if (friendship == null)
            {
                return NotFound();
            }

            _context.Friends.Remove(friendship);
            friendship = _context.Friends.Where(f => (f.UserId1 == originalFriendship.UserId2 && f.UserId2 == originalFriendship.UserId1)).FirstOrDefaultAsync().Result;
            if (friendship == null)
            {
                return NotFound();
            }
            _context.Friends.Remove(friendship);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FriendshipExists(int? id)
        {
            return (_context.Friends?.Any(e => e.FriendshipId == id)).GetValueOrDefault();
        }


    }
}
