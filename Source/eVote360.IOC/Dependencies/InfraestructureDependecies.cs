using eVote360.Core.Domain.Contracts.Repositories.ElectivePosition;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalAlliences;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalAlliance;
using eVote360.Core.Domain.Validators.PoliticalAlliancesValidator;
using eVote360.Core.Domain.Contracts.Repositories.Citizens;
using eVote360.Core.Domain.Contracts.Repositories.Candidate;
using eVote360.Core.Application.Contracts.Services;
using eVote360.Infraestructure.Persistence.Context;
using eVote360.Infraestructure.Persistence.Repositories.Citizens;
using eVote360.Infraestructure.Persistence.Repositories.ElectivePosiction;
using eVote360.Infraestructure.Persistence.Repositories.PoliticalAlliances;
using eVote360.Infraestructure.Persistence.Repositories.Candidate;
using eVote360.Infraestructure.Persistence.Services;
using eVote360.Infraestructure.Persistence.ServicesValidators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection; 
using eVote360.Infraestructure.Persistence.ServicesValidators.Candidatess;
using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Contracts.Repositories.AuthenticationAndAutorization;
using eVote360.Infraestructure.Persistence.Repositories.Authentication;
using eVote360.Core.Domain.Contracts.ServiceValidates.PoliticalParty;
using eVote360.Infraestructure.Persistence.ServicesValidators.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Infraestructure.Persistence.Repositories.PoliticalParty;


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

            //Citizens
            services.AddScoped<ICitizenRepository, CitizensRepository>();

            //Candidates
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<ICandidateDomainService, CandidateServiceValidator>();

            //Common Services
            services.AddScoped<IFileStorageService, LocalFileStorageService>();

            //Authentication

            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

            //Political Parties
            services.AddScoped<IPoliticalPartyRepository, PoliticalPartyRepository>();
            services.AddScoped<IPoliticalPartyDomainService, PoliticalPartyServiceValidator>();

            return services;
        }
    }
}
