using Application.Common.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.TestHost;

namespace API.Test.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> 
        where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureTestServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                        .AddEntityFrameworkInMemoryDatabase()
                        .BuildServiceProvider();

                    // Add a database context using an in-memory 
                    // database for testing.
                    services.AddDbContext<PersistanceDBContext>(options =>
                    {
                        options.UseInMemoryDatabase("InMemoryDbForTesting");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    services.AddScoped<IPersistanceDBContext>(provider => provider.GetService<PersistanceDBContext>());

                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<PersistanceDBContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();

                    
                })
                .UseEnvironment("Test");
        }

        public HttpClient GetAnonymousClient()
        {
            return CreateClient(new WebApplicationFactoryClientOptions() 
            { 
                AllowAutoRedirect=false,
                BaseAddress= new Uri("http://localhost:18023/") 
            });
        }

        //public async Task<HttpClient> GetAuthenticatedClientAsync()
        //{
        //    return await GetAuthenticatedClientAsync("jason@northwind", "Northwind1!");
        //}

        //public async Task<HttpClient> GetAuthenticatedClientAsync(string userName, string password)
        //{
        //    var client = CreateClient();

        //   // var token = await GetAccessTokenAsync(client, userName, password);

        //   // client.SetBearerToken(token);

        //    return client;
        //}

        //private async Task<string> GetAccessTokenAsync(HttpClient client, string userName, string password)
        //{
        //    var disco = await client.GetDiscoveryDocumentAsync();

        //    if (disco.IsError)
        //    {
        //        throw new Exception(disco.Error);
        //    }

        //    var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        //    {
        //        Address = disco.TokenEndpoint,
        //        ClientId = "Northwind.IntegrationTests",
        //        ClientSecret = "secret",

        //        Scope = "Northwind.WebUIAPI openid profile",
        //        UserName = userName,
        //        Password = password
        //    });

        //    if (response.IsError)
        //    {
        //        throw new Exception(response.Error);
        //    }

        //    return response.AccessToken;
        //}
    }
}
