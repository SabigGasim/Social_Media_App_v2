using Domain.Common;
using Domain.Entities;
using Infrastructure.Interfaces;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;

namespace NativeApp.Infrastructure.Repositories;

public class FakeAlertsRepository : IAlertsRepository
{
    private readonly IDatabase _database;

    public FakeAlertsRepository(IDatabase database)
    {
        _database = database;
    }

    public async Task<Result<IEnumerable<AlertDtoBase>>> GetAlerts(Guid userId, Guid? lastSeenAlert, int numberOfAlerts)
    {
        if(lastSeenAlert is null && numberOfAlerts < 20)
        {
            numberOfAlerts = 20;
        }

        var alerts = await _database.GetAlerts(userId, lastSeenAlert, numberOfAlerts);
        return Results.Success(alerts.Map());
    }

    public Task<Result> AddAlert(Guid userId, AlertDtoBase alert)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAlert(Guid alertId)
    {
        throw new NotImplementedException();
    }
}
