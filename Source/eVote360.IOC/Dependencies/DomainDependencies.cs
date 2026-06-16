using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Core.Domain.Contracts.Validators.CandidateAssignment;
using eVote360.Core.Domain.Validators.Admin;
using eVote360.Core.Domain.Validators.AssignmentValidator;
using eVote360.Core.Domain.Validators.CandidateValidator;
using eVote360.Core.Domain.Validators.CitizensValidator;
using eVote360.Core.Domain.Validators.ElectionValidator;
using eVote360.Core.Domain.Validators.ElectivePositionValidator;
using eVote360.Core.Domain.Validators.PoliticalAlliancesValidator;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using eVote360.Core.Domain.Validators.UserValidator;
using eVote360.Infraestructure.Persistence.ServicesValidators.Candidatess;
using Microsoft.Extensions.DependencyInjection;


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

            //Users
            services.AddScoped<IUserValidator, UserValidator>();

            //PoliticalParties
            services.AddScoped<IPoliticalPartyValidator, PoliticalPartyValidator>();

            //Election
            services.AddScoped<IElectionValidator, ElectionValidator>();
            //PoliticalAlliances
            services.AddScoped<IAllianceValidator, AllianceValidator>();

            //CandidateAssignment
            services.AddScoped<IAssignmentValidator, AssignmentValidator>();


            return services;
        }
    }
}
