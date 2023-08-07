using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces;
public interface IRepliesRepository
{
    Task<Result<IEnumerable<ReplyDto>>> GetReplies(Guid commendId, Guid? lastSeenReplyId, int numberOfReplies);
    Task<Result> AddReply(ReplyDto replyDto);
    Task<Result> UpdateReply(ReplyDto replyDto);
    Task<Result> DeleteReply(Guid replyId);
}
