using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public class Follow
{
    public Guid Id { get; set; }
    [Required]
    public Guid FollowerId { get; set; }
    [Required]
    public Guid FollowingId { get; set; }
    public DateTime CreatedDate { get; set; }

    public virtual User Follower { get; set; }
    public virtual User Following { get; set; }
}

