using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;

namespace NativeApp.Infrastructure.Repositories;

public class FakePostsRepository : IPostRepository
{
    private readonly IDatabase _database;

    public FakePostsRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<Result<IEnumerable<PostDto>>> GetUserPosts(Guid userId, Guid? lastSeenPost, int numberOfPosts)
    {
        var posts = (await _database.GetUserPosts(userId, lastSeenPost, numberOfPosts)).Map();
        return Results.Success(posts);
    }

    public Task<Result> AddPost(PostDto post)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeletePost(PostDto post)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdatePost(PostDto post)
    {
        throw new NotImplementedException();
    }
}
