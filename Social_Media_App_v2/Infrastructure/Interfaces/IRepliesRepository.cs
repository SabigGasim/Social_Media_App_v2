using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface IRepliesRepository
{
    Task<Result<IEnumerable<ReplyDto>>> GetReplies(Guid commendId, Guid? lastSeenReplyId, int numberOfReplies);
    Task<Result<ReplyDto>> AddReply(Guid replyingToId, Guid commentId, string text);
    Task<Result> UpdateReply(ReplyDto replyDto);
    Task<Result> DeleteReply(Guid replyId);
}
