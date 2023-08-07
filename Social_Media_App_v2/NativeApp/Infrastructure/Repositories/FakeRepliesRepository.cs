using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApp.Infrastructure.Repositories;
public class FakeRepliesRepository : IRepliesRepository
{
    private readonly IDatabase _database;

    public FakeRepliesRepository(IDatabase database)
    {
        _database = database;
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

    public Task<Result> AddReply(ReplyDto replyDto)
    {
        throw new NotImplementedException();
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
