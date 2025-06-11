using Microsoft.EntityFrameworkCore;
using StudentPlanner.DAL.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//----------------------------------------------------------
// Connection String
var connectionString = builder.Configuration.GetConnectionString("StPlannerConnection");
if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string 'StPlannerConnection' not found.");

builder.Services.AddDbContext<StPlannerContext>(options => options.UseSqlServer(connectionString));
//----------------------------------------------------------

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
