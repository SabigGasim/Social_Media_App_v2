using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface ICommentsRepository
{
    Task<Result<IEnumerable<CommentDto>>> GetComments(Guid PostId, Guid? lastSeenCommentId, int numberOfComments);
    Task<Result<CommentDto>> AddComment(Guid postId, string text);
    Task<Result> DeleteComment(Guid? commentId);
    Task<Result> UpdateComment(string comment);
}
