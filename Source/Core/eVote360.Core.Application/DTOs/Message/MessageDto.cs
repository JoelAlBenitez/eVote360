namespace eVote360.Core.Application.DTOs.Message
{
    public sealed record  MessageDto
    {
        public required string ToEmail { get; set; }
        public required string Body { get; set; }
        public required string Subject { get; set; }
    }
}
