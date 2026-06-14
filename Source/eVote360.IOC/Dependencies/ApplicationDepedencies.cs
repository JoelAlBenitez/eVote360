using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Application.Contracts.Authentication.Query;
using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.Contracts.Election.Commands;
using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.Services.Alliance.CommandHandler;
using eVote360.Core.Application.Services.Alliance.QueryHandler;
using eVote360.Core.Application.Services.Authentication_Autorization.Query;
using eVote360.Core.Application.Services.Candidate.CommandHandler;
using eVote360.Core.Application.Services.Candidate.Query;
using eVote360.Core.Application.Services.Citizens.CommandHandler;
using eVote360.Core.Application.Services.Citizens.Query;
using eVote360.Core.Application.Services.Election.CommandHandler;
using eVote360.Core.Application.Services.Election.Query;
using eVote360.Core.Application.Services.ElectivePosiction.CommandHandler;
using eVote360.Core.Application.Services.ElectivePosiction.Query;
using eVote360.Core.Domain.Contracts.ServiceValidates.Candidate;
using eVote360.Infraestructure.Persistence.ServicesValidators.Candidatess;
using Microsoft.Extensions.DependencyInjection;

namespace eVote360.IOC.Dependencies
{
    public static class ApplicationDepedencies
    {

        public static IServiceCollection AddApplicationDepedencies(this IServiceCollection services)
        {

            //ElectivePosictions
            services.AddScoped<IElectivePosictionsCreateCommand, ElectivePosictionsCreate>();
            services.AddScoped<IElectivePosictionsAlterState, ElectivePosictionsAlterState>();
            services.AddScoped<IElectivePosictionsUpdateCommand, ElectivePosictionsUpdate>();

            services.AddScoped<IElectivePosictionsGetActiveQuery, ElectivePosictionsGetActive>();
            services.AddScoped<IElectivePosictionsGetAllQuery, ElectivePosictionsGetAll>();
            services.AddScoped<IElectivePosictionsGetByIdQuery, ElectivePosictionsGetById>();
            services.AddScoped<IElectivePosictionsGetElectivesPosictionsByDateQuery, ElectivePosictionsGetElectivesPosictionsByDate>();

            //PoliticalAlliances
            services.AddScoped<ICreateAllianceCommand, CreateAllianceCommandHandler>();
            services.AddScoped<IAcceptAllianceCommand, AcceptAllianceCommandHandler>();
            services.AddScoped<IRejectAllianceCommand, RejectAllianceCommandHandler>();
            services.AddScoped<IDeleteAllianceRequestCommand, DeleteAllianceRequestCommandHandler>();
            services.AddScoped<IDeleteActiveAllianceCommand, DeleteActiveAllianceCommandHandler>();

            services.AddScoped<IGetPendingReceivedAlliancesQuery, GetPendingReceivedAlliancesQueryHandler>();
            services.AddScoped<IGetSentAllianceRequestsQuery, GetSentAllianceRequestsQueryHandler>();
            services.AddScoped<IGetActiveAlliancesQuery, GetActiveAlliancesQueryHandler>();
            services.AddScoped<IGetAllianceByIdQuery, GetAllianceByIdQueryHandler>();

            //Citizens
            services.AddScoped<ICitizensAlterStateCommand, CitizensAlterState>();
            services.AddScoped<ICitizensCreateCommand, CitizensCreate>();
            services.AddScoped<ICitizensEditCommand, CitizensUpdate>();

            services.AddScoped<ICitizensGetActiveQuery, CitizensGetAllActive>();
            services.AddScoped<ICitizensGetAllQuery, CitizensGetAll>();
            services.AddScoped<ICitizensGetByIdQuery, CitizensGetById>();

            //Candidates
            services.AddScoped<ICandidateCreateCommand, CandidateCreate>();
            services.AddScoped<ICandidateChangeStateCommand, CandidateChangeState>();
            services.AddScoped<ICandidateUpdateCommand, CandidateUpdate>();

            services.AddScoped<ICandidateGetAllPartyQuery, CandidateGetAllParty>();
            services.AddScoped<ICandidateGetByIdQuery, CandidateGetById>();

            //Election
            services.AddScoped<IElectionCreateCommand, ElectionCreate>();
            services.AddScoped<IElectionUpdateCommand, ElectionUpdate>();
            services.AddScoped<IElectionAlterStateCommand, ElectionAlterState>();

            services.AddScoped<IElectionGetAllQuery, ElectionGetAll>();
            services.AddScoped<IElectionGetByIdQuery, ElectionGetById>();

            //Authentication

            services.AddScoped<ILoginQuery, AuthenticationLoggedUserQuery>();

            return services;
        }

    }
}
