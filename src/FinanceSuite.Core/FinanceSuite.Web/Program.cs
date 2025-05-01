// Program.cs (Minimal API style setup)
using FinanceApplicationDesign.src.FinanceSuite.Core.Interfaces.Persistence;
using FinanceSuite.Core.Interfaces.Services;
using FinanceSuite.Infrastructure.Persistence.Data;
using FinanceSuite.Infrastructure.Persistence.Repositories;
using FinanceSuite.Application.Services;
using Microsoft.EntityFrameworkCore;
using FinanceSuite.Application.Mappings; // For AutoMapper

var builder = WebApplication.CreateBuilder(args);

// 1. Configure Services (Dependency Injection)

// Database Context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositories & Unit of Work (Scoped lifetime is common for web requests)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
// Register other specific repositories...

// Application Services
builder.Services.AddScoped<IRealEstateService, RealEstateService>();
// Register other services...

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile)); // Scan Application assembly

// Add MVC Services
builder.Services.AddControllersWithViews();

// Add Authentication/Authorization (e.g., ASP.NET Core Identity)
// builder.Services.AddDefaultIdentity<IdentityUser>(options => ...)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
// builder.Services.AddAuthorization(...);


var app = builder.Build();

// 2. Configure Middleware Pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Optional: Seed data during development
    // using (var scope = app.Services.CreateScope())
    // {
    //     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //     // await dbContext.Database.MigrateAsync(); // Apply migrations
    //     // Seed data logic...
    // }
    app.UseDeveloperExceptionPage(); // More detailed errors
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Serve files from wwwroot (CSS, JS, Bootstrap)

app.UseRouting();

app.UseAuthentication(); // Must come before Authorization
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Add endpoints for Razor Pages or Minimal APIs if needed
// app.MapRazorPages();

app.Run();
