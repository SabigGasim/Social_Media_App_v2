namespace Domain.Entities;

public class PostDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public IList<MediaDto> Media { get; set; }
    public UserDto User { get; set; }
    public long Likes { get; set; }
    public long CommentsCount { get; set; }
}
