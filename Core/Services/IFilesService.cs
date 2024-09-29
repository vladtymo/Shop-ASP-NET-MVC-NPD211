using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public interface IFilesService
    {
        Task<string> SaveProductImage(IFormFile file);
        Task<string> EditProductImage(IFormFile newFile, string oldPath);
        Task DeleteProductImage(string path);
    }
}
