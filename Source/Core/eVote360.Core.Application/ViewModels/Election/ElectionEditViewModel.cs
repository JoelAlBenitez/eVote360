using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.Settings.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Election
{
    public sealed class ElectionEditViewModel
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Se requiere un Id valido")]
        public int Id { get; set; }

        [Display(Name = "nombre de Eleccion")]
        [Required(ErrorMessage = "Se requiere un nombre valido de eleccion")]
        [StringLength(50, ErrorMessage = "El nombre de eleccion debe tener al menos 8 caracteres", MinimumLength = 8)]
        public required string Name { get; set; }

        [Display(Name = "Estado de Eleccion")]
        [Required(ErrorMessage = "Se requiere un estado valido")]
        public bool State { get; set; }


        [Display(Name = "Fecha de Eleccion")]
        [Required(ErrorMessage = "Se requiere una fecha valida")]
        public DateTime ElectionDate { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Se requiere un estado de eleccion valido")]
        public ElectionState ElectionState { get; set; }
    }
}
