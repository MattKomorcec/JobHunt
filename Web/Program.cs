using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using NLog;
using Web.Configuration;
using Web.Data;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

// Load the Nlog configuration file
LogManager.LoadConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "/nlog.config"));

var connectionString = Database.GetConnectionString(builder.Configuration);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureCookiePolicyOptions();
builder.Services.ConfigureCookies();

// Add needed services
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddSingleton<ILoggerManager, LoggerManager>();

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

// Ensure database is created and migrations are ran
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var context = services.GetRequiredService<ApplicationDbContext>();
context.Database.Migrate();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();