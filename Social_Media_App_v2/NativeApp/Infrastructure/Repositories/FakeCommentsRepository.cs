using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;

namespace NativeApp.Infrastructure.Repositories;

public class FakeCommentsRepository : ICommentsRepository
{
    private IDatabase _database;

    public FakeCommentsRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<Result<IEnumerable<CommentDto>>> GetComments(Guid PostId, Guid? lastSeenCommentId, int numberOfComments)
    {
        if(lastSeenCommentId is null && numberOfComments < 20)
        {
            numberOfComments = 20;
        }
        var comments = (await _database.GetPostComments(lastSeenCommentId, PostId, numberOfComments)).Map();

        return Results.Success(comments);
    }

    public Task<Result> AddComment(CommentDto comment)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteComment(Guid? commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateComment(CommentDto comment)
    {
        throw new NotImplementedException();
    }
}
