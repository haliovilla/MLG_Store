using MLGStore.Services.Validators;

namespace MLGStore.Services.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(byte[] content, string extension, string container, string contentType, GroupFileType groupFileType);
        Task<string> EditFileAsync(byte[] content, string extension, string container, string route, string contentType, GroupFileType groupFileType);
        Task RemoveFileAsync(string route, string container, GroupFileType groupFileType);
    }
}
