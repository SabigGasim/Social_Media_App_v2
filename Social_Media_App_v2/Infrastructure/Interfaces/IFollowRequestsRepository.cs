using Domain.Common;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Interfaces;
public interface IFollowRequestsRepository
{
    Task<Result<IEnumerable<FollowRequestDto>>> GetFollowRequests(Guid? userId, Guid? lastSeenFollowRequest, int numberOfRequests);
    Task<Result<FollowRequestState>> CreateFollowRequest(Guid? userId, Guid userToFollowId);
    Task<Result> UpdateFollowRequestState(Guid userId, Guid userToFollowId, FollowRequestState state);
    Task<Result> DeleteFollowRequest(Guid userId, Guid userToFollowId, Guid requestId);
}
