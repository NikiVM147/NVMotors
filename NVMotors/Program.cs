using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NVMotors.Data;
using NVMotors.Data.Models;
using NVMotors.Services.Data;
using NVMotors.Sevices.Data;
using NVMotors.Sevices.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<NVMotorsDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(cfg =>
{
    cfg.Password.RequiredLength = 0;
    cfg.Password.RequireNonAlphanumeric = false;
    cfg.Password.RequireUppercase = false;
    cfg.Password.RequireLowercase = false;

})
    .AddEntityFrameworkStores<NVMotorsDbContext>()
    .AddUserManager<UserManager<AppUser>>()
    .AddRoles<IdentityRole<Guid>>()
    .AddSignInManager<SignInManager<AppUser>>()
    .AddDefaultTokenProviders()
    .AddDefaultUI();

builder.Services.AddScoped<IMotorService, MotorService>();
builder.Services.AddScoped<IAdService, AdService>();
builder.Services.AddScoped<IAdImageService, AdImageService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IQueryService, QueryService>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddTransient<SeedService>();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    var json = "DataSets/MotorCategories.json";
    seedService.SeedCategories(json);

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    var roles = new[] { "User", "Administrator" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole<Guid>(role));
        }
    }
    await SeedUsers.SeedRolesAndUsers(scope.ServiceProvider);
}

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseStatusCodePages();
app.UseExceptionHandler("/Error/500");

app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");

app.MapControllerRoute(
               name: "Errors",
               pattern: "{controller=Home}/{action=Index}/{statusCode?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
