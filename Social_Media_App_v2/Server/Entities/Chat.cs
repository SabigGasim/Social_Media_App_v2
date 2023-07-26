using System.ComponentModel.DataAnnotations;

namespace Server.Entities;

public abstract class Chat<TChat, TMessage>
    where TChat : Chat<TChat, TMessage>
    where TMessage : Message<TChat, TMessage>
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual IList<TMessage> Messages { get; set; }
}
