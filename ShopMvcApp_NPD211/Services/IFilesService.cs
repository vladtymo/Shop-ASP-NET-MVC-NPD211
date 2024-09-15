using System;

namespace ShopMvcApp_NPD211.Services
{
    public interface IFilesService
    {
        Task<string> SaveProductImage(IFormFile file);
        Task<string> EditProductImage(IFormFile newFile, string oldPath);
        Task DeleteProductImage(string path);
    }

    public class FilesService(IWebHostEnvironment env) : IFilesService
    {
        const string folderName = "images";
        public async Task<string> SaveProductImage(IFormFile file)
        {
            // get new file path
            var root = env.WebRootPath;
            var name = Guid.NewGuid().ToString();
            var ext = Path.GetExtension(file.FileName);

            var relativePath = Path.Combine(folderName, name + ext);
            var fullPath = Path.Combine(root, relativePath);

            // save file content
            using FileStream fs = new FileStream(fullPath, FileMode.Create);
            await file.CopyToAsync(fs);

            return relativePath;
        }

        public Task<string> EditProductImage(IFormFile newFile, string oldPath)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductImage(string path)
        {
            string fullPath = Path.Combine(env.WebRootPath, path);

            if (File.Exists(fullPath))
                return Task.Run(() => File.Delete(fullPath));

            return Task.CompletedTask;
        }
    }
}
