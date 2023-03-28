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
        public async Task<ActionResult<Friendship>> GetFriendship(string? id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            /* AuthUser user1 = findUser(id);*/

            /*  if(_context.authUsers != null)
              {
              *//*    var authuserfriends = _context.Friends.Where(f => f.UserId1 == id).Join(_context.authUsers,
                      f => f.UserId1,
                      u => u.Id,
                      (f, u) => new
                      {
                          f = f,
                          f.UserName1 = u.UserName
                      }
                 ) ;*//*
              var authuserfriends = from friends in _context.Friends
                                     join users in _context.authUsers on friends.UserId1 equals users.Id
                                     where friends.UserId1 == id
                                     select new { Friendship = friends, AuthUser = users };
                  if (authuserfriends != null)
                  {
                      string jsonauth = JsonConvert.SerializeObject(_context.authUsers, Formatting.Indented, new JsonSerializerSettings
                      {
                          ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                      });
                      Console.WriteLine(jsonauth);
                      Console.WriteLine(_context.authUsers.FirstOrDefaultAsync().Result.ToJson());
                  }
                  else
                  {
                      Console.WriteLine("AUTH USER NOT FOUND !!!!!!!!!!!!!!!!!!!");
                  }
              }*/



            /*
                        var friendship = _context.Friends.Where(f => f.UserId1 == id).Include(c => c.User1).ForEachAsync(c => {
                            c.User2 = _context.authUsers.FindAsync(c.UserId2).Result;
                            }
                        , CancellationToken.None)*/
            await _context.Friends.Where(f => f.UserId1 == id).ForEachAsync(async (f) =>
           {
               var user = findUserName(f.UserId1).Result;
               f.UserName1 = user;
               var user2 = findUserName(f.UserId2).Result;

               f.UserName2 = user2;

           });
            var friendship = _context.Friends.Where(f => f.UserId1 == id).FirstOrDefaultAsync().Result;
            /* var friendship = await _context.Friends.Where(f => f.UserId1 == id);*/
            /*var friendship = await _context.Friends.FindAsync(id);*/
/*            string json = JsonConvert.SerializeObject(friendship, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });*/
           /* Console.WriteLine(json);*/
            if (friendship == null)
            {
                return NotFound();
            }


            return friendship;
        }

        // PUT: api/Friendships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFriendship(int? id, Friendship friendship)
        {
            if (id != friendship.FriendshipId)
            {
                return BadRequest();
            }

            _context.Entry(friendship).State = EntityState.Modified;

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

        // POST: api/Friendships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Friendship>> PostFriendship(Friendship friendship)
        {
            if (_context.Friends == null)
            {
                return Problem("Entity set 'PersonDatabaseContext.Friends'  is null.");
            }
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
        public async Task<IActionResult> DeleteFriendship(string? id)
        {
            if (_context.Friends == null)
            {
                return NotFound();
            }
            var friendship = await _context.Friends.FindAsync(id);
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
