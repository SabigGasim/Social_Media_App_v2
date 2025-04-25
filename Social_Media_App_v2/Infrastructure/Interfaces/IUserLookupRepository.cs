using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;
public interface IUserLookupRepository
{
    Task<Result<IEnumerable<UserDto>>> FindManyByUsername(string query);
    Task<Result<UserDto>> FindByUsername(string username);
    Task<Result<UserDto>> GetById(Guid Id);
}
