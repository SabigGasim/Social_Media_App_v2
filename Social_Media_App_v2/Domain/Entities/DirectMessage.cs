using Domain.Enums;

namespace Domain.Entities;

public class DirectMessageDto : MessageDto<DirectChatDto, DirectMessageDto>
{
    public MessageState State { get; set; }
}
