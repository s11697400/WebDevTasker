using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Setup.Models;
using System.Reflection.Emit;

namespace Setup.Areas.Identity.Data;

public class AuthContext : IdentityDbContext<AuthUser>
{
    public AuthContext(DbContextOptions<AuthContext> options)
        : base(options)
    {
    }
    public DbSet<AuthUser> authUsers { get; set; }
    public DbSet<Friendship> Friends { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Friendship>().HasOne(p => p.User1).WithMany(u => u.ListOfPerson).HasForeignKey(p => p.UserId1).OnDelete(DeleteBehavior.Restrict).HasPrincipalKey(f => f.Id);
        
        builder.Entity<Friendship>().HasOne(p => p.User2).WithMany(u => u.FriendsOfPerson).HasForeignKey(p => p.UserId2).OnDelete(DeleteBehavior.Restrict).HasPrincipalKey(f => f.Id);
        builder.Entity<Friendship>()
            .Property(f => f.FriendshipId)
            .ValueGeneratedOnAdd();
        
        builder.Entity<Friendship>().HasKey(f => new
        {
            f.FriendshipId
        });
       Random random = new Random();
        builder.Entity<Person>().HasData(
    new Person { Email = "seedmail@gmail.com", Username = "Person Seed", Password = "Test123", Id = random.Next(1000) });

        builder.Entity<IdentityRole>().HasData(new IdentityRole { Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });
        
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                UserId = "2758afa0-f909-4443-b838-d324486ad9ea"
            }
        );
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

}
