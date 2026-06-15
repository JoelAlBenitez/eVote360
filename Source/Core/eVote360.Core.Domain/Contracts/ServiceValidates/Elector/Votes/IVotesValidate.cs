namespace eVote360.Core.Domain.Contracts.ServiceValidates.Elector.Votes
{
    public interface IVotesValidate
    {
        Task<bool> ExistVoteByCitizen(Guid IdCitizen, int IdElection);
        Task<bool> CitizenParticipatedInElection(Guid Id, string IdentificationCitizens);

    }
}
