namespace Domain.Entities;

public class ReplyDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public Guid CommentId { get; set; }
    public DateTime Date { get; set; }
    public UserDto User { get; set; }
    public UserDto ReplyingTo { get; set; }
    public bool IsLiked { get; set; }
    public long Likes { get; set; }
}
