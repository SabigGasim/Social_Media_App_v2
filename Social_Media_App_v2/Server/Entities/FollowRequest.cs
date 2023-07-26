using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public class FollowRequest
{
    public Guid Id { get; set; }
    public FollowRequestState State { get; set; }
    [Required]
    public Guid FollowerId { get; set; }
    [Required]
    public Guid FollowingId { get; set; }
    public virtual User Follower { get; set; }
    public virtual User Following { get; set; }
}
