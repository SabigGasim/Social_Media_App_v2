using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Infrastructure.Data.Databases.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeApp;
using NativeApp.Interfaces;

namespace NativeApp.Infrastructure.Repositories;
public class FakeTimelineRepository : ITimelineRepository
{
    private readonly FakeInMemoryDatabase _database;

    public FakeTimelineRepository(IDatabase database)
    {
        _database = database as FakeInMemoryDatabase;
    }

    public void AddPosts(IEnumerable<PostDto> posts)
    {
        _database.AddTimeLinePosts(posts.Map());
    }

    public async Task<IEnumerable<PostDto>> GetTimelinePosts(Guid? lastSeenPostId, int numberOfPosts)
    {
        var posts = await _database.GetTimelinePosts(lastSeenPostId, numberOfPosts);

        //await _database.AddTimeLinePosts(posts);

        return posts.Map();
    }
}
