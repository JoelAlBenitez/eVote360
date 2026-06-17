using eVote360.Core.Application.Contracts.PoliticalParty.Query;
using eVote360.Core.Application.DTOs.PoliticalParty;
using eVote360.Core.Domain.Contracts.Repositories.PoliticalParty;

namespace eVote360.Core.Application.Services.PoliticalParty.Query
{
    public sealed class PoliticalPartyGetById : IPoliticalPartyGetByIdQuery
    {
        private readonly IPoliticalPartyRepository _repository;
        public PoliticalPartyGetById(IPoliticalPartyRepository repository)
        {
            _repository = repository;
        }
        public async Task<PoliticalPartyDto> ExecuteAsync(int id)
        {
            var p = await _repository.GetByIdEntitie(id);

            if (p == null)
                return null!;

            return new PoliticalPartyDto
            {
                Id = p.Id,
                Name = p.Name,
                State = p.State,
                PoliticalPartyDescription = p.PoliticalPartyDescription,
                PoliticalPartyAcronym = p.PoliticalPartyAcronym?.Value ?? "",
                PoliticalPartyLogo = p.PoliticalPartyLogo?.PhotoUrl ?? "",
                CreateAt = p.CreateAt,
                CreateUserId = p.CreateUserId,
                UpdateUserId = p.UpdateUserId,
            };
        }
    }
}
