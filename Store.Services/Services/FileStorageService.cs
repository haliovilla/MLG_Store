using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MLGStore.Services.Interfaces;
using MLGStore.Services.Validators;

namespace MLGStore.Services.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ILogger<FileStorageService> logger;

        public FileStorageService(IHttpContextAccessor httpContextAccessor,
            ILogger<FileStorageService> logger)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.logger = logger;
        }

        public async Task<string> SaveFileAsync(byte[] content, string extension, string container, string contentType, GroupFileType groupFileType)
        {
            try
            {
                string filesPath = Path.Combine(Environment.CurrentDirectory, GetFolderName(groupFileType));

                var filename = $"{Guid.NewGuid()}{extension}";
                var folder = Path.Combine(filesPath, container);
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                var route = Path.Combine(folder, filename);
                await File.WriteAllBytesAsync(route, content);

                var url = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
                var result = $"{url}/{GetFolderName(groupFileType)}/{container}/{filename}"
                    .Replace("\\", "/")
                    .Replace("wwwroot/", "");
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return String.Empty;
            }
        }

        public async Task<string> EditFileAsync(byte[] content, string extension, string container, string route, string contentType, GroupFileType groupFileType)
        {
            try
            {
                await RemoveFileAsync(route, container, groupFileType);
                return await SaveFileAsync(content, extension, container, contentType, groupFileType);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return string.Empty;
            }
        }

        public Task RemoveFileAsync(string route, string container, GroupFileType groupFileType)
        {
            try
            {
                string filesPath = Path.Combine(Environment.CurrentDirectory, GetFolderName(groupFileType));

                if (!string.IsNullOrEmpty(route))
                {
                    var filename = Path.GetFileName(route);
                    var filePath = Path.Combine(filesPath.Replace("/", "\\"), container, filename);

                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                return Task.FromResult(-1);
            }
        }

        private string GetFolderName(GroupFileType groupFileType)
        {
            switch (groupFileType)
            {
                case GroupFileType.Image:
                    return "wwwroot/Images";
                default:
                    return "wwwroot/Images";
            }
        }

        private string GetContentTypeByFileExtension(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".pdf":
                    return "application/pdf";
                case ".txt":
                    return "text/plain";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".ppt":
                    return "application/vnd.ms-powerpoint";
                case ".pptx":
                    return "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                case ".html":
                case ".htm":
                    return "text/html";
                case ".md":
                    return "text/markdown";
                default:
                    return "application/octet-stream"; // Tipo de contenido predeterminado si la extensión no se reconoce
            }
        }

    }
}
