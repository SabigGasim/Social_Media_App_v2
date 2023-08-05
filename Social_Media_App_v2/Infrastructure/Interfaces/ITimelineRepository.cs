using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface ITimelineRepository
{
    Task<IEnumerable<PostDto>> GetTimelinePosts(Guid? lastSeenPostId, int numberOfPosts);
    public void AddPosts(IEnumerable<PostDto> posts);
}
