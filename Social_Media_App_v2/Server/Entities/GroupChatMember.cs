using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enums;

namespace Server.Entities;

public class GroupChatMember
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid GroupChatId { get; set; }
    [Required]
    public Membership Membership { get; set; }
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    [ForeignKey(nameof(GroupChatId))]
    public virtual GroupChat GroupChat { get; set; }
}