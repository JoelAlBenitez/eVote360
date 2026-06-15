using eVote360.Core.Domain.Common.Errors;
namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class AdminError
    {
        public static Error YearNoValid
            => new Error("Año no valido", "El año sekeccionado no corresponde a una elección, intente con otra opción.");

        public static Error NoWxisteElection
            => new Error("Elecciones no registradas", "Al parecer aún no hay elecciones registrada, dirigete al apartado de elecciones e inicia un nuevo proceso electoral.");
    }
}
