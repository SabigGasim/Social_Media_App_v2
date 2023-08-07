namespace NativeApp.MVVM.Models;

public class CommentModel
{
    public Guid Id { get; set; }
    public Guid PostId { get; set; }
    public string Text { get; set; }
    public long Likes { get; set; }
    public int RepliesCount { get; set; }
    public DateTime Date { get; init; }
    public UserModel User { get; set; }
}