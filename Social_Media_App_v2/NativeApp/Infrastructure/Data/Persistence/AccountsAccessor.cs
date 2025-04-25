using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.Infrastructure.Data.Persistence;

public class AccountsAccessor : IAccountsAccessor
{
    public UserViewModel? CurrentAccount { get; set; }
    public RangeObservableCollection<UserViewModel>? Accounts { get; set; }
}
