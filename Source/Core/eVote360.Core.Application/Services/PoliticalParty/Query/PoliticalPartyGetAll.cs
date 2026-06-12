using eVote360.Core.Application.Contracts.PoliticalParty.Query;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;
using eVote360.Core.Domain.Validators.PoliticalPartyValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Services.PoliticalParty.Query
{
    public sealed class PoliticalPartyGetAll : IPoliticalPartyGetAllQuery
    {
        private readonly IPoliticalPartyRepository _repository;
        public PoliticalPartyGetAll(IPoliticalPartyRepository repository) 
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<PoliticalPartyDto>> ExecuteAsync()
        {
            var entities = await _repository.GetAllAsync();

            return entities.Select(p => new PoliticalPartyDto
            {
                Name = p.Name,
                State = p.State,
                PoliticalPartyDescription = p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym.Value,
                PoliticalPartyLogo = p.PoliticalPartyLogo,
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
                UpdateUserId = p.UpdateUserId,
            }).ToList().AsReadOnly();
        }
    }
}
