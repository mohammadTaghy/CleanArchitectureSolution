using API.Common;
using Application.DI;
using Asp.Versioning;
using Common;
using Common.DI;
using FluentValidation.AspNetCore;
using Infrastructure.Authentication;
using Infrastructure.DI;
using Microsoft.AspNetCore.Authentication;

//using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Persistence.DI;
using System.Net;
using System.Text;

namespace API
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpsRedirection(options =>
            {
                options.RedirectStatusCode = (int)HttpStatusCode.PermanentRedirect;
                options.HttpsPort = 44376;
            });
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

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
            services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();


            services.AddInfrastructure(Configuration);
            services.AddPersistence(Configuration);
            services.AddCommonDependency(Configuration);
            services.AddApplicationDependency(Configuration);

            services.AddControllers()
                .AddOData(option =>
                {
                    option.Select();
                    option.Expand();
                    option.Filter();
                    option.Count();
                    option.SetMaxTop(100);
                    option.SkipToken();
                    option.AddRouteComponents("Odata", services.GetModel());
                    option.Count();
                    option.OrderBy();
                })
                .AddNewtonsoftJson();


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });



            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
            //services.AddOData().EnableApiVersioning();
            //services.AddODataApiExplorer();

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddMvc()
                .AddSessionStateTempDataProvider();

            services.Configure<FormOptions>(o =>
            {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;


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

            services.Configure<IISServerOptions>(options =>
            {
                options.MaxRequestBodySize = int.MaxValue;
                options.MaxRequestBodyBufferSize = int.MaxValue;
                options.AllowSynchronousIO = true;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

            authentication(services);

            configuration(services);
        }

        private void authentication(IServiceCollection services)
        {
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });
            services.AddTransient<IClaimsTransformation, ClaimsTransformerHelper>();
            services.AddAuthorization(o =>
            {
                o.AddPolicy("CmsClaim", p => p.RequireClaim("CmsClaim", "CmsClaim"));
            });
        }

        private void configuration(IServiceCollection services)
        {
            services.Configure<JwtSettings>(
                Configuration.GetSection("Jwt"));
            services.Configure<MongoDatabaseOption>(
               Configuration.GetSection("MongoDatabaseSettings"));
            services.Configure<AppSettings>(
               Configuration.GetSection("AppSettings"));
           
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();

            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), @"Asset");
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(folderPath),
                RequestPath = new PathString("/Asset")
            });

            app.UseMiddleware<JwtMiddleware>();

            app.UseSpaStaticFiles();
            app.UseCookiePolicy();
            app.UseRouting();

            app.UseSession();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("JWToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    if (context.Request.Headers.ContainsKey("Authorization"))
                        context.Request.Headers.Remove("Authorization");
                    context.Request.Headers.TryAdd("Authorization", "Bearer " + JWToken);
                }
                await next();
            });
            app.UseAuthorization();

           

            app.UseEndpoints(endpoints =>
            {
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
