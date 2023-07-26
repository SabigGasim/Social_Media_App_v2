using Domain.Enums;

namespace Domain.Entities;

public class GroupChatMemberDto
{
    public Guid Id { get; set; }
    public Membership Membership { get; set; }
    public UserDto User { get; set; }
}