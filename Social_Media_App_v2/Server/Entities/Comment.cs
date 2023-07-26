using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public class Comment : ILikable
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public Guid PostId { get; set; }
    [Required]
    public Guid UserId { get; set; }

    public string Text { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public long Likes { get; set; }
    public int RepliesCount { get; set; }

    [DeleteBehavior(DeleteBehavior.Cascade)]
    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(Entities.User.Comments))]
    public virtual User User { get; set; }
    public virtual IList<User> LikedFrom { get; set; }
    public virtual IList<Reply> Replies { get; set; }
    [ForeignKey(nameof(PostId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Post Post { get; set; }
}