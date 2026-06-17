using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eVote360.Core.Application.ViewModels.PoliticalAlliances
{
    public class CreateAllianceViewModel
    {
        [Required(ErrorMessage = "El partido político es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un partido político válido.")]
        public int ReceivingPartyId { get; set; }
        public List<SimpleSelectListItem> AvailableParties { get; set; } = new();
    }

    public class SimpleSelectListItem
    {
        public  string? Value { get; set; } 
        public string? Text { get; set; } 
    }
}
