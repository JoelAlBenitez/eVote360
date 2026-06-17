
using eVote360.Core.Application.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace eVote360.Core.Application.ViewModels.PoliticalParty
{
    public sealed class PoliticalPartyViewModelCreate
    {
        [Display(Name = "Nombre")]
        [Required(ErrorMessage ="Se requiere un nombre valido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage ="Ingrese un nombre de minimo 5 caracteres y un maximo de 100")]
        public required string Name { get; set; }

        [Display(Name = "Descripcion")]
        [Required(ErrorMessage = "Se requiere una descripcion valida")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Ingrese un una descripcion valida de minimo 10 y maximo 500 caracteres")]
        public required string PoliticalPartyDescription { get; set; }

        [Display(Name = "Siglas")]
        [Required(ErrorMessage = "Se requiere unas siglas validas")]
        [StringLength(5, MinimumLength = 2, ErrorMessage = "Ingrese unas siglas de minimo 2 letras y maximo de 5")]
        public required string PoliticalPartyAcronym { get; set; }

        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Se requiere un Logo de partido valido")]
        public required IFormFile? LogoFile { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage ="Se requiere un estado valido")]
        public required bool PoliticalPartyState { get; set; }

    }
}
