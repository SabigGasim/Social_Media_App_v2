using System.ComponentModel.DataAnnotations;

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
    public Media Media { get; set; }
    public User User { get; set; }
    public TChat Chat { get; set; }
}
