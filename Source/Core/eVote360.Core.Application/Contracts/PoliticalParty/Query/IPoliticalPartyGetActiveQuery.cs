using eVote360.Core.Application.DTOs.PoliticalParty;


namespace eVote360.Core.Application.Contracts.PoliticalParty.Query
{
    public interface IPoliticalPartyGetActiveQuery
    {
        Task<IReadOnlyCollection<PoliticalPartyDto>> ExecuteAsync();
    }
}

