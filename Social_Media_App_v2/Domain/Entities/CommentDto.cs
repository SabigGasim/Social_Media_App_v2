namespace Domain.Entities;

public class CommentDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public UserDto User { get; set; }
    public Guid PostId { get; set; }
    public long Likes { get; set; }
    public int RepliesCount { get; set; }
}