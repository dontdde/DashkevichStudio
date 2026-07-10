using DashkevichStudio.Application.Leads;
using Microsoft.Extensions.DependencyInjection;

namespace DashkevichStudio.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TimeProvider.System);
        services.AddScoped<ILeadSubmissionService, LeadSubmissionService>();
        return services;
    }
}
