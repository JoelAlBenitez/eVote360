using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.Services.ElectivePosiction.CommandHandler;
using eVote360.Core.Application.Services.ElectivePosiction.Query;
using eVote360.Core.Application.Contracts.Alliance.Commands;
using eVote360.Core.Application.Contracts.Alliance.Query;
using eVote360.Core.Application.Services.Alliance.CommandHandler;
using eVote360.Core.Application.Services.Alliance.QueryHandler;
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

            return services;
        }

    }
}
