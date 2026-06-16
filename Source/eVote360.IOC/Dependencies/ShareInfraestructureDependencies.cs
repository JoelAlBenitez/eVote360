using eVote360.Core.Application.Contracts.EmailService;
using eVote360.Core.Domain.Settings.EmailService;
using eVote360.Infraestructure.Shared.EmailServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eVote360.IOC.Dependencies
{
    public static class ShareInfraestructureDependencies
    {
        public static IServiceCollection AddSharedInfraesturctureDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
