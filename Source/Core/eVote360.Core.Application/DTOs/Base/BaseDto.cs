namespace eVote360.Core.Application.DTOs.Base
{
    public abstract record BaseDto<Tkey>
    {
        public Tkey? Id { get; set; }
        public required string Name { get; set;}
        public required bool State { get; set; }
      
    }
}
