using eVote360.Core.Application.DTOs.PoliticalParty;

namespace eVote360.Core.Application.Contracts.PoliticalParty.Query
{
    public  interface IPoliticalPartyGetAllQuery
    {
        Task<IEnumerable<PoliticalPartyDto>> ExecuteAsync();
    }
}
