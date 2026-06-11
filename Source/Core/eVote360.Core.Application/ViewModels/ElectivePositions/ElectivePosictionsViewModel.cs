using eVote360.Core.Application.ViewModels.Base;
using System.ComponentModel.DataAnnotations;
namespace eVote360.Core.Application.ViewModels.ElectivePositions
{
    public sealed class ElectivePosictionsViewModel : ViewModelBase<int> {

       
        public required string Description { get; set; }
    }
}
