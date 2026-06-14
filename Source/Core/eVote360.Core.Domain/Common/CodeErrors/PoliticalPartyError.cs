using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static  class PoliticalPartyError
    {
        public static Error PoliticalPartyNameIsRequired
           => new Error(" Creacion de nombre no permitidas", "Se requiere un nnombre de partido politico");
        public static Error PoliticalPartyNameAlreadyExist
            => new Error("Nombre de partido ya registrado", "El nombre ya existe en el sistema por lo que uso no es valido.");
        public static Error PoliticalPartyAcronymTooLong
            => new Error("Siglas muy largas", "Las siglas introducidas son muy largas");
        public static Error PoliticalPartyAcronymAlreadyExist
            => new Error("Siglas no validas", "Las siglas introducidas ya representan otro partido politico");
        public static Error PoliticalPartyAlreadyParticipated
            => new Error("Partido ya ha estado en elecciones", "Este partido ya participo en elecciones por lo que no puede ser modificado.");
        public static Error PoliticalPartyLogoIsRequired
            => new Error("Logo no valido", "El logo del partido es requerido");
    }
}
