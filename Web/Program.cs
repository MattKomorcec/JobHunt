using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Web.Configuration;
using Web.Data;
using Web.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var envVariables = Environment.GetEnvironmentVariables();

if (envVariables is not null)
{
    foreach (var kvp in (Dictionary<string, string>)envVariables)
    {
        Console.WriteLine($"Key: {kvp.Key}, value: {kvp.Value}");
    }
}

Console.WriteLine($"Got the connection string: {connectionString}");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));

builder.Services.AddDefaultIdentity<ApplicationUser>()
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureIdentity();

builder.Services.ConfigureCookiePolicyOptions();

builder.Services.ConfigureCookies();

builder.Services.AddScoped<IJobRepository, JobRepository>();

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