using eVote360.Core.Application.Contracts.ElectivePosictions.Commands;
using eVote360.Core.Application.Contracts.ElectivePosictions.Query;
using eVote360.Core.Application.Contracts.ElectivePosictions.QueryServices;
using eVote360.Core.Application.Services.ElectivePosiction.CommandHandler;
using eVote360.Core.Application.Services.ElectivePosiction.Query;
<<<<<<< HEAD
using Microsoft.Extensions.DependencyInjection;
=======
using eVote360.Core.Application.Services.Citizens.CommandHandler;
using eVote360.Core.Application.Services.Citizens.Query;
using Microsoft.Extensions.DependencyInjection;
using eVote360.Core.Application.Contracts.Citizens.Command;
using eVote360.Core.Application.Contracts.Citizens.Query;
>>>>>>> origin/development

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


<<<<<<< HEAD
=======
            //Citizens
            services.AddScoped<ICitizensAlterStateCommand, CitizensAlterState>();
            services.AddScoped<ICitizensCreateCommand, CitizensCreate>();
            services.AddScoped<ICitizensEditCommand, CitizensUpdate>();

            services.AddScoped<ICitizensGetActiveQuery, CitizensGetAllActive>();
            services.AddScoped<ICitizensGetAllQuery, CitizensGetAll>();
            services.AddScoped<ICitizensGetByIdQuery, CitizensGetById>();

>>>>>>> origin/development
            return services;
        }

    }
}
