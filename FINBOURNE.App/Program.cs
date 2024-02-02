using FINBOURNE.LRUCache.Injection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FINBOURNE.App;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services => {

        // Add Cache key/value type pairs
        services.AddCacheKeyValuePair(
            [
                (typeof(string), typeof(int)),
                (typeof(int), typeof(string)),
                (typeof(string), typeof(Person))
            ],
            capacity: 5,
            serviceLifetime: ServiceLifetime.Singleton);


        services.AddSingleton<Application>();

    }).Build();
var app = _host.Services.GetRequiredService<Application>();
app.Run();