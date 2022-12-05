using System.Net;
using ETrade.Application;
using ETrade.Application.Extensions;
using ETrade.Infrastructure;
using ETrade.MVC;
using ETrade.MVC.Configurations.RateLimit;
using ETrade.MVC.Extensions;
using ETrade.Persistence;
using ETrade.Persistence.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPresentationServices(builder.Configuration,builder.Host);

// Add Rate Limiting
builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Use Rate Limiting
app.UseRateLimiting();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler($"/Error/Index?statusCode={(int)HttpStatusCode.InternalServerError}");
    //alttakinide kullanabilirsin
    //app.CustomExceptionHandler(); 
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/Error/Index","?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSerilogRequestLogging();
app.UseHttpLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


// Auto update migration
await app.MigrateDatabaseAsync();

// Auto seed authorize endpoints data
await app.AuthorizeEndpointsMigrateAsync(typeof(Program));

// Add user_name info to serilog LogContext
app.AddUserIdtoSeriLogContext();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Admin",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.Run();