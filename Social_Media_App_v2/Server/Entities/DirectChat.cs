using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public class DirectChat : Chat<DirectChat, DirectMessage>
{
    [MaxLength(2)]
    [Range(0, 2)]
    [InverseProperty(nameof(User.DirectChats))]
    public virtual IList<User> Users { get; set; }
}

