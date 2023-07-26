using Domain.Enums;

namespace Domain.Entities;

public class FollowRequestDto
{
    public Guid Id { get; set; }
    public FollowRequestState State { get; set; }

    public UserDto Follower { get; set; }
    public UserDto Following { get; set; }
}
