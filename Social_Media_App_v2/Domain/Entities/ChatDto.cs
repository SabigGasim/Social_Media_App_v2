namespace Domain.Entities;

public abstract class ChatDto<TChat, TMessage>
    where TChat : ChatDto<TChat, TMessage>
    where TMessage : MessageDto<TChat, TMessage>
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual IList<TMessage> Messages { get; set; }
}
