using System;

namespace eVote360.Core.Application.ViewModels.PoliticalAlliances
{
    public class AllianceViewModel
    {
        public int Id { get; set; }
        public int RequestingPartyId { get; set; }
        public int ReceivingPartyId { get; set; }
        public string PartyName { get; set; } = string.Empty;
        public string PartySiglas { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime RequestDate { get; set; }
        public DateTime? ResponseDate { get; set; }
    }
}
