namespace eVote360.Core.Domain.Commom.BaseEntity
{
    public abstract class BaseEntitie <Tkey, TName> 
    {
        public required Tkey Id { get; set; }
        public required TName Name {get ; set; }
    }
}
