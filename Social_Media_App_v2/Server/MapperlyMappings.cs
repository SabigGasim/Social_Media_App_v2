using Riok.Mapperly.Abstractions;
using Domain.Entities;
using Server.Entities;

namespace Server;

[Mapper]
public partial class MapperlyMappings
{
    public partial Post Map(PostDto postDto);
    public partial PostDto Map(Post post);

    public partial User Map(UserDto userDto);
    public partial UserDto Map(User user);

    public partial Profile Map(ProfileDto profileDto);
    public partial ProfileDto Map(Profile profile);

    public partial Comment Map(CommentDto commentDto);
    public partial CommentDto Map(Comment comment);

    public partial Reply Map(ReplyDto replyDto);
    public partial ReplyDto Map(Reply reply);

    public partial GroupChat Map(GroupChatDto groupChatDto);
    public partial GroupChatDto Map(GroupChat groupChat);

    public partial DirectChat Map(DirectChatDto directChatDto);
    public partial DirectChatDto Map(DirectChat directChat);

    public partial GroupMessage Map(GroupMessageDto groupMessageDto);
    public partial GroupMessageDto Map(GroupMessage groupMessage);

    public partial DirectMessage Map(DirectMessageDto directMessageDto);
    public partial DirectMessageDto Map(DirectMessage directMessage);

    public partial GroupChatMember Map(GroupChatMemberDto groupChatMemberDto);
    public partial GroupChatMemberDto Map(GroupChatMember groupChatMember);

    public partial Media Map(MediaDto mediaDto);
    public partial MediaDto Map(Media media);

    public partial Follow Map(FollowDto followDto);
    public partial FollowDto Map(Follow follow);

    public partial FollowRequest Map(FollowRequestDto followRequestDto);
    public partial FollowRequestDto Map(FollowRequest followRequest);
}
