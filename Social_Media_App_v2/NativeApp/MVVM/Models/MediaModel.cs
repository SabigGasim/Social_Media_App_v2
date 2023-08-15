namespace NativeApp.MVVM.Models;
public class MediaModel
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string ContentType { get; set; }
    public long ContentLength { get; set; }

    public ImageSource Source => ImageSource.FromUri(new Uri(Url));
}
