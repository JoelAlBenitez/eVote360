using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance;
using eVote360.Core.Domain.Validators.PoliticalAlliancesValidator;
using eVote360.Infraestructure.Persistence.Context;
using eVote360.Infraestructure.Persistence.Repositories.ElectivePosiction;
using eVote360.Infraestructure.Persistence.Repositories.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.ServicesValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace eVote360.IOC.Dependencies
{
    public static class InfraestructureDependecies
    {
        public static IServiceCollection AddInfraestructureDependecies(this IServiceCollection services,
            IConfiguration configuration
            )
        {
            services.AddDbContext<DbContextEVote360>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            //Elecitive Poisitions
            services.AddScoped<IElectivePositionsRepository, ElectivePosictionsRepository>();

            //PoliticalAlliances
            services.AddScoped<IPoliticalAllienceRepository, PoliticalAlliancesRepository>();
            services.AddScoped<IPoliticalAlliancesValidate, PoliticalAlliancesServiceValidator>();
            services.AddScoped<IAllianceValidator, AllianceValidator>();

            //.......
            return services;
        }
    }
}
