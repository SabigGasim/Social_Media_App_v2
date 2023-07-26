namespace Domain.Entities;

public class FollowDto
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }

    public UserDto Follower { get; set; }
    public UserDto Following { get; set; }
}

