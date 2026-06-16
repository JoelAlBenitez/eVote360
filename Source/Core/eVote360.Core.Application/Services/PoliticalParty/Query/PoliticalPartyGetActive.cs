using eVote360.Core.Application.Contracts.PoliticalParty.Query;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;

namespace eVote360.Core.Application.Services.PoliticalParty.Query
{
    public sealed class PoliticalPartyGetActive : IPoliticalPartyGetActiveQuery
    {
        private readonly IPoliticalPartyRepository _repository;
        public PoliticalPartyGetActive(IPoliticalPartyRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyCollection<PoliticalPartyDto>> ExecuteAsync()
        {
            var entities = await _repository.GetActivePartiesAsync();

            return entities.Select(p => new PoliticalPartyDto
            {
                Name = p.Name,
                State = p.State,
                PoliticalPartyDescription = p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym.Value,
                PoliticalPartyLogo = p.PoliticalPartyLogo.PhotoUrl,
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
                UpdateUserId = p.UpdateUserId,
            }).ToList().AsReadOnly();
        }
    }
}
