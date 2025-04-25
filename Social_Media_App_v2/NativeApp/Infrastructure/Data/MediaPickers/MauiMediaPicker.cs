using CommunityToolkit.Maui.Alerts;
using NativeApp.MVVM.Models;
using System.Collections.Immutable;

namespace NativeApp.Infrastructure.Data.MediaPickers;

public class MauiMediaPicker : Interfaces.IMediaPicker
{
    public async Task<IEnumerable<MediaModel>> PickPhotosAsync()
    {
        if (!await IsPermissionsGranted())
        {
            return Enumerable.Empty<MediaModel>();
        }

        var options = new PickOptions { FileTypes = FilePickerFileType.Images };
        var files = (await FilePicker.Default.PickMultipleAsync(options)).ToImmutableArray();

        if (files.Length > 5)
        {
            await DisplayMaximumPhotosReachedToast();
        }

        var length = files.Length <= 5 ? files.Length : 5;
        var media = new MediaModel[length];

        for (int i = 0; i < length; i++)
        {
            var photo = files[i];

            string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

            using (Stream sourceStream = await photo.OpenReadAsync())
            {
                using (FileStream localFileStream = File.OpenWrite(localFilePath))
                {
                    await sourceStream.CopyToAsync(localFileStream);
                }
            }

            media[i] = new MediaModel
            {
                Source = ImageSource.FromFile(localFilePath)
            };
        }

        return media;
    }

    private async Task<bool> IsPermissionsGranted()
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if(status == PermissionStatus.Granted)
        {
            return true;
        }

        var grantStatus = await Permissions.RequestAsync<Permissions.StorageRead>();
        if(grantStatus == PermissionStatus.Granted)
        {
            return true;
        }

        return false;
    }

    private static async Task DisplayMaximumPhotosReachedToast()
    {
        await Toast.Make("You can select up to only 5 photos.",
                            CommunityToolkit.Maui.Core.ToastDuration.Long, 14).Show();
    }
}