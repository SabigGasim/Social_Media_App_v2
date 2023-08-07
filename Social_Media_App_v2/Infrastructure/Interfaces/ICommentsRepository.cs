using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface ICommentsRepository
{
    Task<Result<IEnumerable<CommentDto>>> GetComments(Guid PostId, Guid? lastSeenCommentId, int numberOfComments);
    Task<Result> AddComment(CommentDto comment);
    Task<Result> DeleteComment(Guid? commentId);
    Task<Result> UpdateComment(CommentDto comment);
}
