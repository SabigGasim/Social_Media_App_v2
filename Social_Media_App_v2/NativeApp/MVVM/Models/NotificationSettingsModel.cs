using Domain.Common;

namespace NativeApp.MVVM.Models;
public class NotificationSettingsModel
{
    public NotificationTypes NotificationTypes { get; set; }
    public NotificationMethods NotificationMethods { get; set; }
}
