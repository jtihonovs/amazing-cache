// See https://aka.ms/new-console-template for more information
using FINBOURNE.App.Services;
using FINBOURNE.GenericCache;
using FINBOURNE.Cache.Injection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services => {

        // Add Cache key/value type pairs
        services.AddCacheItemTypes(
            [
                (typeof(string), typeof(int)),
                (typeof(int), typeof(string)),
            ],
            ServiceLifetime.Singleton);


        services.AddSingleton<Application>();

    }).Build();
var app = _host.Services.GetRequiredService<Application>();
app.Run();