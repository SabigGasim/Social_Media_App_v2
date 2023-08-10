using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NativeApp.Infrastructure.Repositories;

public class FakeFollowRequestsRepository : IFollowRequestsRepository
{
    private readonly IDatabase _database;

    public FakeFollowRequestsRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<Result<IEnumerable<FollowRequestDto>>> GetFollowRequests(Guid? userId, Guid? lastSeenFollowRequest, int numberOfRequests)
    {
        if (lastSeenFollowRequest is null && numberOfRequests < 20)
        {
            numberOfRequests = 20;
        }

        var requests = (await _database.GetFollowRequests(userId, lastSeenFollowRequest, numberOfRequests)).Map();

        return Results.Success(requests);
    }

    public Task<Result<FollowRequestState>> CreateFollowRequest(Guid? userId, Guid userToFollowId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteFollowRequest(Guid userId, Guid userToFollowId, Guid requestId)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateFollowRequestState(Guid userId, Guid userToFollowId, FollowRequestState state)
    {
        throw new NotImplementedException();
    }
}
