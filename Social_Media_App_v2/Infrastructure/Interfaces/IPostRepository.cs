using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface IPostRepository
{
    Task<Result<IEnumerable<PostDto>>> GetUserPosts(Guid userId, Guid? lastSeenPost, int numberOfPosts);
    Task<Result> AddPost(PostDto post);
    Task<Result> DeletePost(PostDto post);
    Task<Result> UpdatePost(PostDto post);
}
