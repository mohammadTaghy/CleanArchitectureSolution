using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Newtonsoft;

using API.Common;
using API.Services;
using Application.Common.Interfaces;
using Application.DI;
using Common.DI;
using Infrastructure.DI;
using Persistence;

namespace API
{
    public class Startup
    {
        private IServiceCollection _services;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddInfrastructure(Configuration);
            services.AddCommonDependency(Configuration);
            services.AddApplicationDependency(Configuration);

            //services.AddHealthChecks()
            //   .AddDbContextCheck<PersistanceDBContext>();


            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "FrontEnd-Ang/dist";
            });
            services.AddControllers()
                .AddNewtonsoftJson()
                .AddFluentValidation(p => p.RegisterValidatorsFromAssemblyContaining<PersistanceDBContext>());

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMemoryCache();
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options =>
                {
                    options.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });
            _services = services;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomExceptionHandler();
            app.UseCors();

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapControllers();
            });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "FrontEnd-Ang";
                if (Environment.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.UseProxyToSpaDevelopmentServer("https://localhost:44376");
                }
            });

            //app.Run();
        }
    }
}
