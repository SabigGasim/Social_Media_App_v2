using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Entities;

public abstract class Message<TChat, TMessage>
    where TMessage : Message<TChat, TMessage>
    where TChat : Chat<TChat, TMessage>
{
    public Guid Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid ChatId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }
    public Guid MediaId { get; set; }
    [ForeignKey(nameof(MediaId))]
    [DeleteBehavior(DeleteBehavior.NoAction)]
    public virtual Media Media { get; set; }
    public virtual User User { get; set; }
    [DeleteBehavior(DeleteBehavior.Cascade)]
    public virtual TChat Chat { get; set; }
}
