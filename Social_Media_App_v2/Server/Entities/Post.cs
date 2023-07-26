using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public class Post : ILikable
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }

    public string Text { get; set; }
    public long Likes { get; set; }
    public long CommentsCount { get; set; }

    public virtual IList<User> LikedFrom { get; set; }
    public virtual IList<Comment> Comments { get; set; }
    [MaxLength(10)]
    [Range(0, 10)]
    public virtual IList<Media> Media { get; set; }
    [DeleteBehavior(DeleteBehavior.Cascade)]
    [InverseProperty(nameof(Entities.User.Posts))]
    public virtual User User { get; set; }
}
