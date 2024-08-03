using AuthBlazer.Areas.Identity;
using AuthBlazer.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Blazored.Modal;
using Radzen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<EmployeeService>();
// builder.Services.AddScoped<DialogService>();
// builder.Services.AddScoped<SignInManager<ApplicationUser>>();
// builder.Services.AddScoped<UserManager<ApplicationUser>>();
// builder.Services.AddScoped<AdminService>();
// builder.Services.AddBlazoredModal();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");



var scope = app.Services.CreateScope();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

string[] roleNames = { "Admin", "User" };
IdentityResult roleResult;

// Vérifier et créer les rôles si nécessaire
foreach (var roleName in roleNames)
{
    var roleExist = await roleManager.RoleExistsAsync(roleName);
    if (!roleExist)
    {
        roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
    }
}

// Créer un utilisateur Admin par défaut et l'assigner au rôle Admin
var adminUser = new IdentityUser
{
    UserName = "admin@example.com",
    Email = "admin@example.com",
    EmailConfirmed = true
};

string adminPassword = "Admin@123";
var user = await userManager.FindByEmailAsync(adminUser.Email);

if (user == null)
{
    var createAdminUser = await userManager.CreateAsync(adminUser, adminPassword);
    if (createAdminUser.Succeeded)
    {
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

app.Run();

