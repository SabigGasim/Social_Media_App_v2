namespace NativeApp.MVVM.Models;

public class ReplyModel
{
    public Guid Id { get; set; }
    public Guid CommentId { get; set; }
    public string Text { get; set; }
    public long Likes { get; set; }
    public bool IsLiked { get; set; }
    public DateTime Date { get; init; }
    public UserModel User { get; set; }
    public UserModel ReplyingTo { get; set; }
}