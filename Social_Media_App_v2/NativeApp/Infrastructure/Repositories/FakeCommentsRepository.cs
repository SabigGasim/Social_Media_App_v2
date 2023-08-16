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

    public async Task<Result<CommentDto>> AddComment(Guid postId, string text)
    {
        var user = new UserDto()
        {
            State = Domain.Enums.AccountState.Active,
            Nickname = "Just Me",
            UserName = "ME",
            Profile = new ProfileDto
            {
                Description = "nothing but test",
                Icon = new MediaDto
                {
                    Url = "https://cdn.pixabay.com/photo/2015/10/05/22/37/blank-profile-picture-973460_1280.png",
                    ContentType = ".png"
                }
            }
        };
        user.Profile.User = user;

        var comment = new CommentDto
        {
            CreatedDate = DateTime.Now,
            Text = text,
            User = user,
            PostId = postId,
        };

        var result = await _database.AddComment(comment);

        return result;
    }

    public Task<Result> DeleteComment(Guid? commentId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateComment(string comment)
    {
        throw new NotImplementedException();
    }
}
