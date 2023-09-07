using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

public class BlockedAccountsViewModel : MutedAndBlockedUsersViewModelBase
{
    public BlockedAccountsViewModel(
        MutedAndBlockedViewModel viewModel, 
        IServiceProvider serviceProvider) : base(viewModel, serviceProvider)
    {
        UsersGetter = () => viewModel.Model?.BlockedUsers;
        UsersSetter = (value) => viewModel.Model!.BlockedUsers = value;
    }

    protected override Func<RangeObservableCollection<UserModel>?> UsersGetter { get; init; }
    protected override Action<RangeObservableCollection<UserModel>?> UsersSetter { get; init; }
}
