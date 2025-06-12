using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.BLL.Interfaces;
using StudentPlanner.BLL.Mapper;
using StudentPlanner.BLL.Repository;
using StudentPlanner.DAL.Database;
using StudentPlanner.DAL.Extends;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//----------------------------------------------------------
<<<<<<< HEAD
builder.Services.AddHostedService<ReminderBackgroundService>();

//Registering the Course Repository in Dependency Injection
//Object Lifetime - Scoped > one object for each user deals with all operations
builder.Services.AddScoped<ICourse, CourseRepo>();
builder.Services.AddScoped<IStudent, StudentRepo>();
builder.Services.AddScoped<IReminder, ReminderRepo>();
builder.Services.AddScoped<IAssignment, AssignmentRepo>();
builder.Services.AddScoped<IExam, ExamRepo>();

builder.Services.AddScoped<IEmail, EmailRepo>();
//----------------------------------------------------------
//Mapping
//builder.Services.AddAutoMapper(x=> x.AddProfile(new DomainProfile()));
builder.Services.AddAutoMapper(typeof(DomainProfile));
=======
//Registering the Course Repository in Dependency Injection
//Object Lifetime - Scoped > one object for each user deals with all operations
builder.Services.AddScoped<ICourse, CourseRepo>();
//----------------------------------------------------------
builder.Services.AddAutoMapper(x=> x.AddProfile(new DomainProfile()));
>>>>>>> 75dd13d (Created Mapper and Ojbect Lifetime)
//----------------------------------------------------------
// Connection String
var connectionString = builder.Configuration.GetConnectionString("StPlannerConnection");

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string 'StPlannerConnection' not found.");

builder.Services.AddDbContext<StPlannerContext>(options => options.UseSqlServer(connectionString));
//-------------------------------------------------------------------------
//Authentication
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 0;
})
.AddEntityFrameworkStores<StPlannerContext>()
.AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);

builder.Services.ConfigureApplicationCookie(options =>
{
    options.ExpireTimeSpan = TimeSpan.FromDays(30);
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/Login";
});
//-------------------------------------------------------------------------

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
