using eVote360.Core.Application.ViewModels.Base;

namespace eVote360.Core.Application.ViewModels.Citizens
{
    public sealed class CitizensViewModel : ViewModelBase<Guid>
    {
        public required string Email { get; set; }
        public required string Identification { get; set; }
        public required string LastName { get; set; }
    }
}
