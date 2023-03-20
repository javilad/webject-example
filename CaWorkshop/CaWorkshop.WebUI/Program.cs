using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using CaWorkshop.Infrastructure.Data;
using CaWorkshop.Domain.Entities;
using CaWorkshop.Infrastructure.Identity;
using CaWorkshop.Application;
using CaWorkshop.Infrastructure;
//using FluentValidation.AspNetCore;
using CaWorkshop.Application.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

//Validation
//FluenValidtation way
//builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews()
//    .AddFluentValidation(config =>
//        config.RegisterValidatorsFromAssemblyContaining<CaWorkshop.Application.Common.Interfaces.IApplicationDbContext>());

//Past Call with 
//builder.Services.AddControllersWithViews()
//    .AddFluentValidation(config =>
//        config.RegisterValidatorsFromAssemblyContaining<IApplicationDbContext>());


//Add documentation

builder.Services.AddOpenApiDocument(configure =>
{
    configure.Title = "CaWorkshop API";
});

var app = builder.Build();

#if DEBUG

//if (app.Environment.IsDevelopment()) {

// Initialise and seed the database on start-up
using (var scope = app.Services.CreateScope())
{
    try
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        initialiser.Initialise();
        initialiser.Seed();
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred during database initialisation.");

        throw;
    }
}
//}

#endif


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseIdentityServer();
app.UseAuthorization();

app.UseOpenApi();
app.UseSwaggerUi3();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.MapRazorPages();

app.MapFallbackToFile("index.html"); ;

app.Run();
