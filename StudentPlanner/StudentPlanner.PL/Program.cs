using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Mapper;
using StudentPlanner.BLL.Repository;
using StudentPlanner.DAL.Database;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//----------------------------------------------------------
//Registering the Course Repository in Dependency Injection
//Object Lifetime - Scoped > one object for each user deals with all operations
builder.Services.AddScoped<ICourse, CourseRepo>();
//----------------------------------------------------------
builder.Services.AddAutoMapper(x=> x.AddProfile(new DomainProfile()));
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
