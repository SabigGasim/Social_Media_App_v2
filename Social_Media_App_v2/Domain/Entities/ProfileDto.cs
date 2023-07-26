namespace Domain.Entities;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public UserDto User { get; set; }
    public IList<PostDto> Posts { get; set; }
    public MediaDto Icon { get; set; }
}