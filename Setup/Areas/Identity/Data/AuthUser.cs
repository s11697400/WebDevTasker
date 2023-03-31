using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Setup.Models;

namespace Setup.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AuthUser class
public class AuthUser : IdentityUser
{
    public int highScore { get; set; }
    public List<Friendship> ListOfPerson { get; set; }
    public List<Friendship> FriendsOfPerson { get; set; }

}

