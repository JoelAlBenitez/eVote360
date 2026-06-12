using eVote360.Core.Application.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace eVote360.Infraestructure.Persistence.Services
{
    public class LocalFileStorageService : IFileStorageService
    {
        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) return string.Empty;

            // Ruta base (ej. wwwroot/images/candidates)
            // Se asume que la ejecucion es desde el proyecto Presentation
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName);
            
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string safeFileName = Path.GetFileName(file.FileName);
            string uniqueFileName = $"{Guid.NewGuid()}_{safeFileName}";
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            // Retornamos la ruta relativa para guardar en BD
            return $"/images/{folderName}/{uniqueFileName}";
        }
    }
}
