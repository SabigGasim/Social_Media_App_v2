using NativeApp.MVVM.Models;

namespace NativeApp.Interfaces;
public interface IMediaPicker
{
    public Task<IEnumerable<MediaModel>> PickPhotosAsync();
}
