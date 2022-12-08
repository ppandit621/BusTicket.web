using BusTicket.DataAcess;
using BusTicket.DataAcess.Infrastructure;
using BusTicket.DataAcess.Repositories;
using BusTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDbContext<BusApplicationDBContext>(Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders().AddEntityFrameworkStores<BusApplicationDBContext>();
/*using (var scope = host.Services.CreateScope())
{
    var Services=scope.ServiceProvider;
    try
    {
        var context = Services.GetRequiredService<BusApplicationDBContext>();
        var userManager = Services.GetRequiredService<UserManager<ApplicationManager>>();
        DbInitializer.InitializeAsync(context, Services, userManager).Wait();
    }
    catch(Exception ex)
    {
        var logger = Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database");
    }
}*/
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var Services = scope.ServiceProvider;
    try
    {
        var context = Services.GetRequiredService<BusApplicationDBContext>();
        var userManager = Services.GetRequiredService<UserManager<IdentityUser>>();
        DbInitializer.InitializeAsync(context, Services, userManager).Wait();
    }
    catch (Exception ex)
    {
        var logger = Services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database");
    }
}


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
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Main}/{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
