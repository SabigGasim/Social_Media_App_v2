using Domain.Enums;

namespace Server.Entities;

public class DirectMessage : Message<DirectChat, DirectMessage>
{
    public MessageState State { get; set; }
}