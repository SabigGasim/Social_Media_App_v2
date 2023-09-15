using Domain.Common;

namespace NativeApp.Interfaces;
public interface IFileManager
{
    Task<Result<string>> ReadFile(string path);
    Task<Result> WriteToFile(string path, string text);
    Task<Stream> OpenReadAsync(string path);
}
