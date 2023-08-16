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

public class FakeUserLookupRepository : IUserLookupRepository
{
    private readonly IDatabase _database;

    public FakeUserLookupRepository(IDatabase database)
    {
        _database = database;
    }

    public Task<Result<UserDto>> FindByUsername(string username)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<IEnumerable<UserDto>>> FindManyByUsername(string query)
    {
        var users = (await _database.FindUsersByUsername(query)).Map();
        return Results.Success(users);
    }

    public async Task<Result<UserDto>> GetById(Guid Id)
    {
        var result = await _database.GetUserById(Id);
        return result;
    }
}
