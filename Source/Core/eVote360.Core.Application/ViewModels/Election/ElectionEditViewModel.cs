using eVote360.Core.Application.ViewModels.Base;
using eVote360.Core.Domain.Common.Enums;
using eVote360.Core.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eVote360.Core.Application.ViewModels.Election
{
    public sealed class ElectionEditViewModel : ViewModelBase<int>
    {

        [Display(Name = "Fecha de Eleccion")]
        [Required(ErrorMessage = "Se requiere una fecha valida")]
        public DateTime ElectionDate { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "Se requiere un estado de eleccion valido")]
        public ElectionState ElectionState { get; set; }
    }
}
