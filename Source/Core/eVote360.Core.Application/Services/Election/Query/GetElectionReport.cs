using eVote360.Core.Application.Contracts.Election.Query;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;

namespace eVote360.Core.Application.Services.Election.Query
{
    public sealed class GetElectionReport : IGetElectionReportQuery
    {
        private readonly IElectionRepository  _repository;

        public GetElectionReport(IElectionRepository repository) {
        _repository = repository;
        }

        public async Task<IReadOnlyCollection<ElectionResultDto>> ExecuteAsync(int electionId)
        {
            var domainResults = await _repository.GetElectionResultAsync(electionId);

            return domainResults.Select(r=> new ElectionResultDto
            {
                CandidateName = r.CandidateNamer,
                TotalVotes = r.TotalVotes,
                Percentage = r.Percentage,

                PartyName = r.PartyName,
                PartyAcronym = r.PartyAcronym,
                PartyLogo = r.PartyLogo
            }).ToList().AsReadOnly();
        }
    }
}
