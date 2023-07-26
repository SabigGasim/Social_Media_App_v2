using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public class Reply : ILikable
{
    public Guid Id { get; set; }
    [Required]
    public Guid CommentId { get; set; }
    [Required]
    public Guid UserId { get; set; }

    public string Text { get; set; }
    public long Likes { get; set; }

    public virtual IList<User> LikedFrom { get; set; }
    [ForeignKey(nameof(CommentId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Comment Comment { get; set; }
    [ForeignKey(nameof(UserId))]
    [InverseProperty(nameof(Entities.User.Replies))]
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual User User { get; set; }
}
