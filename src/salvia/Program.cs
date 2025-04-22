using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using salvia.Core;
using salvia.Data;
using salvia.Telegram;
using System;
using System.Threading.Tasks;

namespace salvia;

class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureAppConfiguration((context, config) =>
        {
            config
                .SetBasePath(context.HostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                    optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.local.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddUserSecrets<Program>();
        });

        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        builder.ConfigureServices((context, services) =>
        {
            services.Configure<BotConfiguration>(context.Configuration.GetSection(nameof(BotConfiguration)));

            services.AddDbContext<DiseaseDbContext>(
            options =>
            {
                options.UseNpgsql(context.Configuration.GetConnectionString(nameof(DiseaseDbContext)));
            });

            services.AddDataServices();
            services.AddCoreServices();
            services.AddTelegramApiServices();
        });

        var host = builder.Build();

        var telegram = host.Services.GetRequiredService<ITelegramApi>();
        await telegram.Run();

        //var configuration = CreateConfiguration(args);
        //var services = CreateServiceProvider(configuration);

        //var telegram = services.GetRequiredService<ITelegramApi>();
        //await telegram.Run();
    }

    //private static IServiceProvider CreateServiceProvider(IConfigurationRoot configuration)
    //{
    //    var services = new ServiceCollection();

    //    services.Configure<BotConfiguration>(configuration.GetSection(nameof(BotConfiguration)));

    //    services.AddDbContext<DiseaseDbContext>(
    //        options =>
    //        {
    //            options.UseNpgsql(configuration.GetConnectionString(nameof(DiseaseDbContext)));
    //        });

    //    services.AddTelegramApiServices();

    //    return services.BuildServiceProvider();
    //}

    //private static IConfigurationRoot CreateConfiguration(string[] args)
    //{
    //    var builder = new ConfigurationBuilder();

    //    builder
    //        .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
    //        .AddJsonFile("appSettings.json", false, false)
    //        .AddEnvironmentVariables()
    //        .AddUserSecrets<Program>();

    //    return builder.Build();
    //}
}
