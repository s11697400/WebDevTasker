using Microsoft.EntityFrameworkCore;
using Setup.DataContext;
using Microsoft.AspNetCore.Identity;
using Setup.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
var connectionString = builder.Configuration.GetConnectionString("PersonDatabaseContext");
builder.Services.AddDbContext<PersonDatabaseContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<AuthUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthContext>();
builder.Services.AddDbContext<AuthContext>(options => options.UseSqlServer(connectionString));

/*builder.Services.AddDefaultIdentity<AuthUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AuthContext>();*/
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapGet("/GetThijs", () => "Hello Wereld!");
app.MapPost("/API", () => "THIS IS POST");
app.MapRazorPages();
app.Run();
