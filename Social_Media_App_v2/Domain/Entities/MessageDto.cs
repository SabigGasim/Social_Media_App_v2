namespace Domain.Entities;

public class MessageDto<TChat, TMessage>
    where TMessage : MessageDto<TChat, TMessage>
    where TChat : ChatDto<TChat, TMessage>
{
    public Guid Id { get; set; }
    public Guid ChatId { get; set; }
    public string Text { get; set; }
    public DateTime SentAt { get; set; }

    public MediaDto Media { get; set; }
}
