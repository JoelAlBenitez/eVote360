using eVote360.Core.Domain.Common.Errors;

namespace eVote360.Core.Domain.Common.CodeErrors
{
    public static class ElectivePosictionsError
    {
        public static Error ElectivePosictionUsedByEleccionsActive
            => new Error("Modificaciones no permitidas", "No se puede modificar el puesto si hay eleccciones activas.");
        public static Error NameInvalid
            => new Error("Puesto ya registrado", "El nombre ya existe en el sistema por lo que uso no es valido.");
        public static Error ElectivePosictionHasAssociatedByCandidates
            => new Error("Puesto con candidactos", "Mientras existan candidactos asociados al puesto el mismo no se puede desactivar.");
        public static Error DataInvalid
            => new Error("Datos no validos", "Se han introducido datos no validos en el intento operacional.");
        public static Error NameCannotChange
            => new Error("Puesto ya usado en elecciones", "Este puesto ya fue utilizado en elecciones por lo que su nombre no puede ser modificado.");
        public static Error ActivedElectivePosiction
            => new Error("Puesto electivo no puede ser activado", "Para activar este puesto no deben existir otros puestos con el mismo nombre o elecciones activas.");
        public static Error DesactiveElectivePosiction
            => new Error("Puesto electivo no puede ser desactivado", "Para desactivar este puesto no debe tener candidactos activos asignados para eleccciones o elecciones activas.");
        public static Error DescriptionInvalid
            => new Error("Descripción no permitida", "La descripción a intentar registrar no es valida de cumplir con solo letras.");

    }
}
