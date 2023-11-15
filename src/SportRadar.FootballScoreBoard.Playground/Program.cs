using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Taskmaverick.SelfSignUp.Services;

public class Program
{
    public static void Main(string[] args)
    {
        using IHost host = CreateHostBuilder(args).Build();
        var consoleApp = host.Services.GetRequiredService<ConsoleApplication>();
        consoleApp.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddScoped<ConsoleApplication>();
                services.AddFootballScoreBoardServices();
            });
}