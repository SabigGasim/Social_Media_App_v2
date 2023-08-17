namespace Domain.Common;
public class NotificationTypes
{
    public bool NewPostsFromFriends { get; set; }
    public bool NewFollows { get; set; }
    public bool FollowRequests { get; set; }
    public bool AcceptedFollowReuqests { get; set; }
    public bool DirectMessages { get; set; }
    public bool GroupMessages { get; set; }
    public bool ContentSuggestion { get; set; }
}
