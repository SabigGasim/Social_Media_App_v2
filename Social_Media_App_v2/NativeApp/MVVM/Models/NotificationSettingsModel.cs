using Domain.Common;

namespace NativeApp.MVVM.Models;
public class NotificationSettingsModel
{
    public NotificationTypes Types { get; set; }
    public NotificationMethods Methods { get; set; }
}
