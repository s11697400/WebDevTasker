using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Setup.Areas.Identity.Data;

namespace Setup.Areas.Identity
{
    public class SampleData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AuthContext>();

            string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

            foreach (string role in roles)
            {
                var roleStore = new RoleStore<IdentityRole>(context);

                if (!context.Roles.Any(r => r.Name == role))
                {
                    roleStore.CreateAsync(new IdentityRole(role));
                }
            }


            /*   var user = new AuthUser
               {
                   FirstName = "XXXX",
                   LastName = "XXXX",
                   Email = "xxxx@example.com",
                   NormalizedEmail = "XXXX@EXAMPLE.COM",
                   UserName = "Owner",
                   NormalizedUserName = "OWNER",
                   PhoneNumber = "+111111111111",
                   EmailConfirmed = true,
                   PhoneNumberConfirmed = true,
                   SecurityStamp = Guid.NewGuid().ToString("D")
               };*/

/*
            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "secret");
                user.PasswordHash = hashed;

                var userStore = new UserStore<ApplicationUser>(context);
                var result = userStore.CreateAsync(user);

            }*/

            AssignRoles(serviceProvider, "tsoepenberg@gmail.com", roles);

            context.SaveChangesAsync();
        }

        public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles)
        {
            UserManager<AuthUser> _userManager = services.GetService<UserManager<AuthUser>>();
            AuthUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.AddToRolesAsync(user, roles);

            return result;
        }
    }
}
