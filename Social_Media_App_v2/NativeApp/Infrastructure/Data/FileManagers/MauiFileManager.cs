using Domain.Common;
using NativeApp.Interfaces;

namespace NativeApp.Infrastructure.Data.FileManagers;
public class MauiFileManager : IFileManager
{
    public async Task<Stream> OpenReadAsync(string path)
    {
        return await FileSystem.Current.OpenAppPackageFileAsync(path);
    }

    public async Task<Result<string>> ReadFile(string path)
    {
        try
        {
            using var fileStream = await OpenReadAsync(path);
            using var reader = new StreamReader(fileStream);
            var text = reader.ReadToEnd();
            return Results.Success(text);
        }
        catch (Exception ex)
        {
            return Results.Fail<string>(ex);
        }
    }

    public async Task<Result> WriteToFile(string path, string text)
    {
        try
        {
            using var fileStream = await OpenReadAsync(path);
            using var writer = new StreamWriter(fileStream);
            writer.Write(text);

            return Results.Success();
        }
        catch(Exception ex)
        {
            return Results.Fail(ex);
        }
    }
}
