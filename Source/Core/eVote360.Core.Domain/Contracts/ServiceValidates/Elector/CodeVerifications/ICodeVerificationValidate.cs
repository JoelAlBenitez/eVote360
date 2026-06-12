namespace eVote360.Core.Domain.Contracts.ServiceValidates.Elector.CodeVerifications
{
    public interface ICodeVerificationValidate
    {
        Task<bool> ExistCodeVerificationActive(Guid IdCitizen, int IdElection);
        Task<bool> CodeExpire(Guid IdCitizen, int IdElection);
        Task<bool> CodeUse (Guid IdCitizen, int IdElection);
        Task<bool> CodeMatchesWithRecord(int code, Guid IdCitizen, int IdElection);

    }

}
