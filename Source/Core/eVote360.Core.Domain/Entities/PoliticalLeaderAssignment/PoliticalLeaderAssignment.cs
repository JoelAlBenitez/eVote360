using System;

namespace eVote360.Core.Domain.Entities.PoliticalLeaderAssignment
{
    public class PoliticalLeaderAssignment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PoliticalPartyId { get; set; }
        public DateTimeOffset CreateAt { get; set; }
        public int CreateUserId { get; set; }
    }
}