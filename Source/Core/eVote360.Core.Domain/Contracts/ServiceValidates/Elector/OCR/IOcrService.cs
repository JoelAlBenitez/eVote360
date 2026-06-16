namespace eVote360.Core.Domain.Contracts.ServiceValidates.Elector.OCR
{
    public interface IOcrService
    {
        string ExtractTextByte(byte[] imageBytes);
    }
}
