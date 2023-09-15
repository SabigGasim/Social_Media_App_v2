namespace NativeApp.MVVM.Models;
public class MediaModel
{
    private ImageSource? _source;

    public Guid Id { get; set; }
    public string? Url { get; set; }
    public string ContentType { get; set; } = default!;
    public long ContentLength { get; set; }

    public ImageSource? Source
    {
        get => _source ?? (!string.IsNullOrEmpty(Url) ? ImageSource.FromUri(new Uri(Url)) : _source);
        set => _source = value;
    }
}
