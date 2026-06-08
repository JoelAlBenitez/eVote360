namespace eVote360.Core.Application.DTOs.Base
{
    public abstract record BaseDto<Tkey>
    {
        public Tkey? Id { get; }
        public required string? Name { get; set;}
        public required bool State { get; set; }
        public required DateTimeOffset CreateAt { get; set; }
        public DateTimeOffset? UpdateAt { get; set; }
        public required int CreateUserId { get; set; }
        public int? UpdateUserId { get; set; }
    }
}
