using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public class GroupChat : Chat<GroupChat, GroupMessage>
{
    [MaxLength(255)]
    public string Description { get; set; }
    public Guid IconId { get; set; }
    [ForeignKey("IconId")]
    public Media? Icon { get; set; }
    [InverseProperty(nameof(GroupChatMember.GroupChat))]
    public virtual IList<GroupChatMember>? Members { get; set; }
}
