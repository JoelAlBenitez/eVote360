using eVote360.Core.Application.DTOs.Base;
using eVote360.Core.Domain.Common.Enums;


namespace eVote360.Core.Application.DTOs.Election
{
    public record ElectionDto : BaseDto<int>
    {
        public DateTime ElectionDate { get; set; }

        public ElectionState ElectionState { get; set; }

        public DateTime CreateAt { get; set; }

        public int CreateUserId { get; set; }

        public DateTime UpdateAt { get; set; }

        public int UpdateUserId { get; set; }
    }
}
