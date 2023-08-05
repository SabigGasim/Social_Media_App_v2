using NativeApp.MVVM.Models;


namespace NativeApp.Interfaces;
public interface IDatabase
{
    Task<IEnumerable<PostModel>> GetTimelinePosts(Guid? lastSeenPostId, int numberOfPosts);
    Task AddTimeLinePosts(IEnumerable<PostModel> posts);
}
