using BlockyHeroesBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using BlockyHeroesBackend.Infrastructure.Extensions;
using BlockyHeroesBackend.Application.Extensions;
using Carter;
using Asp.Versioning;
using BlockyHeroesBackend.Api.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using BlockyHeroesBackend.Api.Middleware;
using BlockyHeroesBackend.Api.Middleware.Authorization;

var builder = WebApplication.CreateBuilder(args);


// Adding DbContext
builder.Services.AddDbContext<BlockyHeroesDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlockyHeroesDb")));

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddCarter();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen(options =>
{
    // Add a custom operation filter which sets default values
    options.OperationFilter<SwaggerDefaultValues>();
});

// Adding custom extensions
builder.Services.AddInfrastructure();
builder.Services.AddApplication();
builder.Services.AddCustomAuthentication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var descriptions = app.DescribeApiVersions();

        // Build a swagger endpoint for each discovered API version
        foreach (var description in descriptions)
        {
            var url = $"/swagger/{description.GroupName}/swagger.json";
            var name = description.GroupName.ToUpperInvariant();
            options.SwaggerEndpoint(url, name);
        }
    });
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapCarter();
app.AddJwtValidationMiddleware();

app.Run();