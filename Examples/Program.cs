// See https://aka.ms/new-console-template for more information
using FINBOURNE.App.Services;
using FINBOURNE.GenericCache;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(
    services => {
        services.AddTransient(typeof(ILRUCache<,>), typeof(LRUCache<,>));
        services.AddSingleton<Application>();

    }).Build();
var app = _host.Services.GetRequiredService<Application>();
app.Run();