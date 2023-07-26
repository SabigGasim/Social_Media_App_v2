namespace Domain.Entities;

public class ReplyDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public CommentDto Comment { get; set; }
    public UserDto User { get; set; }
    public long Likes { get; set; }
}
