using Microsoft.Extensions.DependencyInjection;
using BlockyHeroesBackend.Infrastructure.Extensions;
using BlockyHeroesBackend.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BlockyHeroesBackend.Seeder;


/*
 * DI Container initialization
 */
var builder = new ConfigurationBuilder();
var service = new ServiceCollection();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
IConfiguration config = builder.Build();

service.AddDbContext<BlockyHeroesDbContext>(options =>
    options.UseSqlServer(config.GetConnectionString("BlockyHeroesDb")));
service.AddInfrastructure();
service.AddSingleton<Application>();

var serviceProvider = service.BuildServiceProvider();

Application app = serviceProvider.GetRequiredService<Application>();
await app.RunSeeder();