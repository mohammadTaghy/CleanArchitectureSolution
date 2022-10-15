using API.Common;
using API.Services;
using Application.Common.Interfaces;
using Application.DI;
using Common.DI;
using Domain.Entities;
using FluentValidation.AspNetCore;
using Infrastructure.Authentication;
using Infrastructure.DI;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Persistence;
using System.ComponentModel;
using System.Text;

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

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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

            services.AddSession();
            services.AddMvc(option => option.EnableEndpointRouting = false).AddSessionStateTempDataProvider();

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

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new
                    SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes
                    (Configuration["Jwt:Key"]))
                };
            });
            _services = services;
        }
        public void Configure(IApplicationBuilder app)
        {
            app.UseCustomExceptionHandler();
            app.UseSession();
            app.UseMvc();
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

            app.UseMiddleware<JwtMiddleware>();//.ServerFeatures.Get<IEndpointFeature>().Endpoint.Metadata.Any(p=>p is AuthorizeAttribute);

            //app.UseAuthentication();
            //app.UseIdentityServer();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller}/{action=Index}/{id?}");
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
