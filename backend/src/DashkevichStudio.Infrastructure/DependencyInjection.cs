using DashkevichStudio.Application.Abstractions;
using DashkevichStudio.Infrastructure.Files;
using DashkevichStudio.Infrastructure.Notifications;
using DashkevichStudio.Infrastructure.Persistence;
using DashkevichStudio.Infrastructure.Telegram;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DashkevichStudio.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Postgres")
            ?? throw new InvalidOperationException("ConnectionStrings:Postgres is required.");

        services.AddDbContext<StudioDbContext>(options => options.UseNpgsql(connectionString));
        services.Configure<FileStorageOptions>(configuration.GetSection(FileStorageOptions.SectionName));
        services.Configure<TelegramOptions>(configuration.GetSection(TelegramOptions.SectionName));

        services.AddScoped<ILeadRepository, LeadRepository>();
        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<StudioDbContext>());
        services.AddSingleton<IFileStorage, LocalFileStorage>();
        services.AddHttpClient<ITelegramNotifier, TelegramNotifier>(client =>
        {
            client.BaseAddress = new Uri("https://api.telegram.org/");
            client.Timeout = TimeSpan.FromSeconds(30);
        });
        services.AddHostedService<OutboxWorker>();

        return services;
    }
}
