using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.Services.ElectivePosiction.CommandHandler;
using eVote360.Core.Application.Services.ElectivePosiction.Query;
using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Application.Services.Alliance.CommandHandler;
using eVote360.Core.Application.Services.Alliance.QueryHandler;
using eVote360.Core.Application.Services.Citizens.CommandHandler;
using eVote360.Core.Application.Services.Citizens.Query;
using Microsoft.Extensions.DependencyInjection;
using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Application.Contracts.Citizens.Query;
using eVote360.Core.Application.Contracts.Candidate.Commands;
using eVote360.Core.Application.Contracts.Candidate.Query;
using eVote360.Core.Application.Services.Candidate.CommandHandler;
using eVote360.Core.Application.Services.Candidate.Query;
using eVote360.Core.Application.Contracts.Authentication.Query;
using eVote360.Core.Application.Services.Authentication_Autorization.Query;

using eVote360.Core.Application.Contracts.CandidateAssignment.Commands;
using eVote360.Core.Application.Contracts.CandidateAssignment.Query;
using eVote360.Core.Application.Services.CandidateAssignment.CommandHandler;
using eVote360.Core.Application.Services.CandidateAssignment.QueryHandler;
using eVote360.Core.Domain.Contracts.Validators.CandidateAssignment;
using eVote360.Core.Domain.Validators.AssignmentValidator;

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

            //CandidateAssignment
            services.AddScoped<ICreateAssignmentCommand, CreateAssignmentCommandHandler>();
            services.AddScoped<IDeleteAssignmentCommand, DeleteAssignmentCommandHandler>();
            services.AddScoped<IGetAssignmentsByPartyQuery, GetAssignmentsByPartyQueryHandler>();
            services.AddScoped<IGetAssignmentByIdQuery, GetAssignmentByIdQueryHandler>();
            services.AddScoped<IGetEligibleCandidatesForPositionQuery, GetEligibleCandidatesForPositionQueryHandler>();
            services.AddScoped<IAssignmentValidator, AssignmentValidator>();

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

            //Authentication

            services.AddScoped<ILoginQuery, AuthenticationLoggedUserQuery>();

            return services;
        }

    }
}
