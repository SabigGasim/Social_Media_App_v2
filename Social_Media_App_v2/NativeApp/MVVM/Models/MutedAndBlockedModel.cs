using NativeApp.Helpers;

namespace NativeApp.MVVM.Models;
public class MutedAndBlockedModel
{
    public RangeObservableCollection<UserModel>? BlockedUsers { get; set; }
    public RangeObservableCollection<UserModel>? MutedUsers { get; set; }
    public RangeObservableCollection<string>? MutedWords { get; set; }
}
