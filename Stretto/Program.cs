// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Stretto;
using Stretto.Helpers;
using Stretto.Services;

var services = new ServiceCollection();
ConfigureServices(services);
services
    .AddSingleton<Worker, Worker>()
    .BuildServiceProvider()
    .GetService<Worker>()
    .Run();

static void ConfigureServices(IServiceCollection services)
{
    services.AddHttpClient();
    services
        .AddTransient<IHouseService, HouseService>()
        .AddTransient<IDataAccesssService, DataAccessService>()
        .AddTransient<IConsoleHelper, ConsoleHelper>();

}