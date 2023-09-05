using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class UserDto
{
    public Guid Id { get; set; }
    [MaxLength(20)]
    [MinLength(1)]
    public string? UserName { get; set; }
    public string Nickname { get; set; }
    public long FollowersCount { get; set; }
    public long FollowingCount { get; set; }
    public long PostsCount { get; set; }
    public bool IsUserBeingFollowed { get; set; }
    public bool IsMuted { get; set; }
    public bool IsBlocked { get; set; }
    public ProfileDto Profile { get; set; }
    public AccountState State { get; set; }
}