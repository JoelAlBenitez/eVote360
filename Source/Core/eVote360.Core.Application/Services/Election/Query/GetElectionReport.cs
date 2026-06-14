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
            var grouped = domainResults.GroupBy(r => r.PositionName);
            var resultList = new List<ElectionResultDto>();

            foreach (var group in grouped)
            {
                int totalVotesPosition = group.Sum(r => r.TotalVotes);
                int maxVotes = group.Max(r => r.TotalVotes);
                int countMaxVotes = group.Count(r => r.TotalVotes == maxVotes);

                var sortedGroup = group.OrderByDescending(r => r.TotalVotes);

                foreach (var r in sortedGroup)
                {
                    resultList.Add(new ElectionResultDto
                    {
                        PositionName = r.PositionName,
                        CandidateName = r.CandidateNamer,
                        TotalVotes = r.TotalVotes,
                        Percentage = totalVotesPosition > 0 ? (double)r.TotalVotes / totalVotesPosition * 100 : 0,
                        PartyName = r.PartyName,
                        PartyAcronym = r.PartyAcronym,
                        PartyLogo = r.PartyLogo,
                        ResultStatus = countMaxVotes > 1 ? "Empate" : (r.TotalVotes == maxVotes ? "Ganador" : "Perdedor")
                    });
                }
            }
            return resultList.AsReadOnly();
        }
    }
}
