using eVote360.Core.Application.DTOs.Base;
namespace eVote360.Core.Application.DTOs.ElectivePositions
{
    public record ElectivePosictionsDto : BaseDto<int> { 
        public required string Descriptions { get; set; }
    }
   
}
