using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class DashboardErrors
    {
        public static Error LeaderHasNoPartyAssigned
            => new Error("Acceso denegado", "El dirigente no tiene un partido político asignado.");
    }
}