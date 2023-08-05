using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get => base.Id; set => base.Id = value; }
    [Required]
    [MaxLength(20)]
    [MinLength(1)]
    public override string? UserName { get => base.UserName; set => base.UserName = value; }
    public string Nickname { get; set; }
    [Required]
    public AccountState State { get; set; }
    public virtual Profile Profile { get; set; }
    public virtual Timeline Timeline { get; set; }
    public virtual IList<Follow> Followers { get; set; }
    public virtual IList<Follow> Followings { get; set; }
    public virtual IList<FollowRequest> SentFollowRequests { get; set; }
    public virtual IList<FollowRequest> RecievedFollowRequests { get; set; }
    public virtual IList<GroupChat> GroupChats { get; set; }
    public virtual IList<DirectChat> DirectChats { get; set; }
    public virtual IList<Post> Posts { get; set; }
    public virtual IList<Comment> Comments { get; set; }
    public virtual IList<Reply> Replies { get; set; }

    public virtual IList<Post> LikedPosts { get; set; }
    public virtual IList<Comment> LikedComments { get; set; }
    public virtual IList<Reply> LikedReplies { get; set; }
}