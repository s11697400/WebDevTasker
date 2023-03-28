using Microsoft.EntityFrameworkCore;
using Setup.Areas.Identity.Data;
using Setup.Migrations.Auth;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setup.Models
{
    [PrimaryKey(nameof(FriendshipId))]
    public class Friendship
    {

        public virtual AuthUser User1 { get; set; }
        public virtual AuthUser User2 { get; set; }
        [InverseProperty("AuthUser")]
        [ForeignKey("UserId1")]
        [Key, Column(Order = 0)]
        public  string? UserId1 { get; set; }
        [InverseProperty("AuthUser")]
        [ForeignKey("UserId2")]
        [Key, Column(Order = 1)]
        public string? UserId2 { get; set; }
        public DateTime since;

        [InverseProperty("AuthUser")]
        public string? UserName1 { get; set; }
        [InverseProperty("AuthUser")]
        public string? UserName2 { get; set; }
        public int? FriendshipId { get; set; }
        
        public Friendship()
        {
            

        }
        public Friendship(AuthUser user1, AuthUser user2)
        {
            User1= user1;
            User2= user2;
            UserId1 = user1.Id;
            UserId2 = user2.Id;
            since = DateTime.UtcNow;
            UserName1 = user1.UserName;
            UserName2 = user2.UserName;
        }
    }
}
