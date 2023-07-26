using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class GroupChatDto : ChatDto<GroupChatDto, GroupMessageDto>
{
    public MediaDto Icon { get; set; }
    public string Description { get; set; }
    public IList<GroupChatMemberDto> Members { get; set; }
}
