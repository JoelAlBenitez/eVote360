using eVote360.Core.Domain.Validators.CitizensValidator;
using eVote360.Core.Domain.Validators.ElectivePositionValidator;
using eVote360.Core.Domain.Validators.CandidateValidator;
using Microsoft.Extensions.DependencyInjection;
using eVote360.Core.Domain.Validators.Admin;

namespace eVote360.IOC.Dependencies
{
    public static class DomainDependencies
    {

        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {

            //Elective Poisitions
            services.AddScoped<IElectivePositionsValidator, ElectivePositionsValidator>();

            //Citizens
            services.AddScoped<ICitizensValidator, CitizensValidator>();

            //Candidates
            services.AddScoped<ICandidateValidator, CandidateValidator>();

            //admin
            services.AddScoped<IAdminValidator, AdminValidator>();

            return services;
        }
    }
}
