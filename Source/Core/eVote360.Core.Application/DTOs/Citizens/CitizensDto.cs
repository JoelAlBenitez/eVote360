using eVote360.Core.Application.DTOs.Base;

namespace eVote360.Core.Application.DTOs.Citizens
{
    public sealed record  CitizensDto : BaseDto<Guid>
    {
        public  required string Email {  get; set; }
        public  required string Identification { get; set; }
        public required string LastName {  get; set; }


    }
}
