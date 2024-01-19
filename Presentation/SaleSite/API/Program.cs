using API;
using Common;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System.Reflection;

var host = WebHost.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostingContext, config) =>
    {
        var env = hostingContext.HostingEnvironment;

        config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

        if (env.IsDevelopment())
        {
            var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
            if (appAssembly != null)
            {
                config.AddUserSecrets(appAssembly, optional: true);
            }
        }

        config.AddEnvironmentVariables();

        if (args != null)
        {
            config.AddCommandLine(args);
        }
    })
    .UseStartup<Startup>().Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var persistanceDBContext = services.GetRequiredService<PersistanceDBContext>();
        persistanceDBContext.Database.Migrate();


    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating or initializing the database.");
    }
}

host.Run();





//using API.Common;
//using Application.DI;
//using Common.DI;
//using FluentValidation.AspNetCore;
//using Infrastructure.DI;
//using Persistence;
//using Microsoft.AspNetCore.Builder;
//using Application.Common.Interfaces;
//using API.Services;
//using Microsoft.AspNetCore.SpaServices.AngularCli;
//using Microsoft.AspNetCore.Mvc;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddInfrastructure(builder.Configuration, builder.Host);
//builder.Services.AddCommonDependency(builder.Configuration, builder.Host);
//builder.Services.AddApplicationDependency(builder.Configuration, builder.Host);
//builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
//builder.Services.AddHttpContextAccessor();
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//{
//    options.SuppressModelStateInvalidFilter = true;
//});
//builder.Services.AddSpaStaticFiles(configuration =>
//{
//    configuration.RootPath = "FrontEnd-Ang/dist";
//});
//builder.Services.AddControllers()
//    .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<PersistanceDBContext>());

//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddMemoryCache();
//builder.Services.AddCors(c =>
//{
//    c.AddPolicy("AllowOrigin", options => {
//        options.AllowAnyOrigin()
//        .AllowAnyMethod()
//        .AllowAnyHeader();
//    });
//});

//var app = builder.Build();
//app.UseCustomExceptionHandler();
//app.UseCors();

//if (app.Environment.IsDevelopment())
//{
//    app.UseDeveloperExceptionPage();
//    //app.UseDatabaseErrorPage();
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
//app.UseStaticFiles();
//app.UseSpaStaticFiles();
//app.UseRouting();

//app.UseAuthorization();

//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller}/{action=Index}/{id?}");
//    endpoints.MapControllers();
//});
//app.UseSpa(spa =>
//{
//    spa.Options.SourcePath = "FrontEnd-Ang";
//    if (app.Environment.IsDevelopment())
//    {
//        spa.UseAngularCliServer(npmScript: "start");
//        spa.UseProxyToSpaDevelopmentServer("https://localhost:44376");
//    }
//});

//app.Run();
