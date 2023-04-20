using Microsoft.EntityFrameworkCore;
using Setup.DataContext;
using Microsoft.AspNetCore.Identity;
using Setup.Areas.Identity.Data;
using Hubs;
using System.Text.Json.Serialization;
using Setup.Areas.Identity;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var connectionString = builder.Configuration.GetConnectionString("PersonDatabaseContext");
        builder.Services.AddDbContext<PersonDatabaseContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<AuthUser>(options => options.SignIn.RequireConfirmedAccount = true).AddRoles<IdentityRole>().AddEntityFrameworkStores<AuthContext>();

        builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdministratorRole",
                 policy => policy.RequireRole("Administrator"));
        });
        builder.Services.AddSignalR();
        builder.Services.AddSession(options =>
        {
            options.Cookie.Name = ".AdventureWorks.Session";
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.IsEssential = true;
        });
        builder.Services.AddControllers().AddJsonOptions(x =>
                        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();
        app.UseAuthentication();



        app.UseAuthorization();
        app.UseSession();

        /*app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");*/
        app.MapDefaultControllerRoute();

        app.MapGet("/GetThijs", () => "Hello Wereld!");
        app.MapPost("/API", () => "THIS IS POST");
        app.MapRazorPages();
        app.MapHub<ChatHub>("/chatHub");

        app.Run();
    }
}