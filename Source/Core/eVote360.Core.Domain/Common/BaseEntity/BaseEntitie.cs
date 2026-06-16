using eVote360.Core.Domain.Entities.User;

namespace eVote360.Core.Domain.Commom.BaseEntity
{
    public abstract class BaseEntitie<Tkey, TName>
    {
        public Tkey? Id { get; set; }
        public required TName Name { get; set; }
        public DateTimeOffset? CreateAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
        public int? CreateUserId { get; set; }
        public int? UpdateUserId { get; set; }
        public required bool State { get; set; }
        public User? UserCreate { get; set; }
        public User? UserUpdate { get; set; }

    }
}