using eVote360.Core.Application.Contracts.Admin.Query;
using eVote360.Core.Application.DTOs.Admin;
using eVote360.Core.Application.DTOs.Election;
using eVote360.Core.Domain.Common.Errors;
using eVote360.Core.Domain.Common.ValidationResult;
using eVote360.Core.Domain.Contracts.Repositories.ElectionRepository;
using eVote360.Core.Domain.Validators.Admin;

namespace eVote360.Core.Application.Services.Admin
{
    public class ElectionByYear : IElectionByYearQuery
    {

        private readonly IElectionRepository _electionRository;
        private readonly IAdminValidator _adminValidator;
        public ElectionByYear(IElectionRepository electionRository,
            IAdminValidator adminValidator
            )
        {
            _electionRository = electionRository;
            _adminValidator = adminValidator;
        }

        public async Task<ValidationResult<IReadOnlyCollection<ElectionResultDto>>> GetRegisterAsync(DateTime year)
        {
            try
            {
                var validate = await _adminValidator.ValidateElectionByYear(year.Year);
                if (!validate.IsValid) return ValidationResult<IReadOnlyCollection<ElectionResultDto>>.Failure(validate.errors.ToList());
                
                
                var list = await _electionRository.ElectionByYearAsync(year);


                var grouped = list.GroupBy(r => r.PositionName);
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


                return ValidationResult<IReadOnlyCollection<ElectionResultDto>>.Success(resultList);
            }
            catch (Exception ex)
            {
                var errors = new List<Error>();
                errors.Add(new Error("Ha ocurrido un error inesperado", ex.Message));
                return ValidationResult<IReadOnlyCollection<ElectionResultDto>>.Failure(errors);
            }
        }
    }
}
