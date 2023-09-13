using NativeApp.Helpers;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.Interfaces;

public interface IAccountsAccessor
{
    public UserViewModel? CurrentAccount { get; set; }
    public RangeObservableCollection<UserViewModel>? Accounts { get; set; }
}
