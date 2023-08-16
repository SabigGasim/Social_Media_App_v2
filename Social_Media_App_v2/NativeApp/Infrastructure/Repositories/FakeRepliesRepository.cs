using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NativeApp.Infrastructure.Repositories;
public class FakeRepliesRepository : IRepliesRepository
{
    private readonly IDatabase _database;
    private readonly IUserLookupRepository _userLookupRepository;

    public FakeRepliesRepository(IDatabase database, IUserLookupRepository userLookupRepository)
    {
        _database = database;
        _userLookupRepository = userLookupRepository;
    }
    public async Task<Result<IEnumerable<ReplyDto>>> GetReplies(Guid commendId, Guid? lastSeenReplyId, int numberOfReplies)
    {
        if (lastSeenReplyId is null && numberOfReplies < 20)
        {
            numberOfReplies = 20;
        }
        var replies = (await _database.GetCommentReplies(commendId, lastSeenReplyId, numberOfReplies)).Map();

        return Results.Success(replies);
    }

    public async Task<Result<ReplyDto>> AddReply(Guid replyingToId, Guid commentId, string text)
    {
        var userResult = await _userLookupRepository.GetById(replyingToId);
        if (!userResult.Success)
        {
            return Results.Fail<ReplyDto>(userResult.Errors);
        }

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

        var reply = new ReplyDto
        {
            Date = DateTime.Now,
            Text = text,
            User = user,
            CommentId = commentId,
            ReplyingTo = userResult.Value!
        };

        var result = await _database.AddReply(reply);

        return result;
    }

    public Task<Result> DeleteReply(Guid replyId)
    {
        throw new NotImplementedException();
    }


    public Task<Result> UpdateReply(ReplyDto replyDto)
    {
        throw new NotImplementedException();
    }
}
