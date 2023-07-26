using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class DirectChatDto : ChatDto<DirectChatDto, DirectMessageDto>
{
    public UserDto User { get; set; }
}
