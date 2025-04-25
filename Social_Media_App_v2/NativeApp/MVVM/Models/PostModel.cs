namespace NativeApp.MVVM.Models;


public sealed class PostModel
{
    public Guid Id { get; set; }
    public UserModel User { get; set; }
    public string Text { get; set; }
    public DateTime Date { get; init; }
    public bool IsLiked { get; set; }
    public long Likes { get; set; }
    public long CommentsCount { get; set; }
    public MediaListModel? Media { get; set; }
}