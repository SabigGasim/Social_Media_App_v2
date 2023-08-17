using Domain.Common;
using Domain.Enums;
using NativeApp.Helpers;

namespace NativeApp.MVVM.Models;

public class AccountSettingsModel
{
    public Privacy AccountPrivacy { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public NotificationMethods NotificationMethods { get; set; } = default!;
    public NotificationTypes NotificationTypes { get; set; } = default!;
    public RangeObservableCollection<Guid>? BlockedAccounts { get; set; }
    public RangeObservableCollection<Guid>? MutedAccounts { get; set; }
}
