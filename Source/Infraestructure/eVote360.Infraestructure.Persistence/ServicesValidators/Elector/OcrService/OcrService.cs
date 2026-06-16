using eVote360.Core.Domain.Contracts.ServiceValidates.Elector.OCR;
using Tesseract;

namespace eVote360.Infraestructure.Persistence.ServicesValidators.Elector.OcrService
{
    public class OcrService : IOcrService
    {

        private readonly string _dataPath;
        public OcrService(string path)
        {
            _dataPath = path;
        }

        public string ExtractTextByte(byte[] imageBytes)
        {
            try
            {
              
                using var engine = new TesseractEngine(_dataPath, "spa", EngineMode.Default);

                using var img = Pix.LoadFromMemory(imageBytes);

                using var page = engine.Process(img);

                return page.GetText()?.Trim() ?? string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar el motor OCR Tesseract.", ex);
            }
        }
    }
}
