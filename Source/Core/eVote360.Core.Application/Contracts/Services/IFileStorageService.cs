using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace eVote360.Core.Application.Contracts.Services
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
    }
}
