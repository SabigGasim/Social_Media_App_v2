using NativeApp.MVVM.Models;


namespace NativeApp.Interfaces;
public interface IDatabase
{
    Task<IEnumerable<PostModel>> GetTimelinePosts(Guid? lastSeenPostId, int numberOfPosts);
    Task AddTimeLinePosts(IEnumerable<PostModel> posts);
    Task<IEnumerable<CommentModel>> GetPostComments(Guid? lastSeenPostId, Guid? postId, int numberOfComments);
    Task<IEnumerable<ReplyModel>> GetCommentReplies(Guid commentId, Guid? lastSeenReplyId, int numberOfReplies);
    Task<IEnumerable<FollowRequestModel>> GetFollowRequests(Guid? userId, Guid? lastSeenFollowRequest, int numberOfRequests);
    Task<IEnumerable<UserModel>> FindUsersByUsername(string query);
    Task<IEnumerable<PostModel>> GetUserPosts(Guid userId, Guid? lastSeenPost, int numberOfPosts);
}
