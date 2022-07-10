using API.Common;
using Application.DI;
using Common.DI;
using FluentValidation.AspNetCore;
using Infrastructure.DI;
using Persistence;
using Microsoft.AspNetCore.Builder;
using Application.Common.Interfaces;
using API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration, builder.Host);
builder.Services.AddCommonDependency(builder.Configuration, builder.Host);
builder.Services.AddApplicationDependency(builder.Configuration, builder.Host);
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
    .AddFluentValidation(p=>p.RegisterValidatorsFromAssemblyContaining<PersistanceDBContext>());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseCustomExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllerRoute(name: "default",
               pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
