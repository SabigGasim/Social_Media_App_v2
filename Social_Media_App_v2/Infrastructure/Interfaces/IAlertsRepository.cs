using Domain.Common;
using Domain.Entities;

namespace Infrastructure.Interfaces;

public interface IAlertsRepository
{
    Task<Result<IEnumerable<AlertDtoBase>>> GetAlerts(Guid userId, Guid? lastSeenAlert, int numberOfAlerts);
    Task<Result> AddAlert(Guid userId, AlertDtoBase alert);
    Task<Result> DeleteAlert(Guid alertId);
}
