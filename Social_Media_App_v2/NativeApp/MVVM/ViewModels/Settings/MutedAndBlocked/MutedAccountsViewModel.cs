using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
public class MutedAccountsViewModel : MutedAndBlockedUsersViewModelBase
{
    public MutedAccountsViewModel(
        MutedAndBlockedViewModel viewModel, 
        IServiceProvider serviceProvider) 
        : base(viewModel, serviceProvider)
    {
        UsersGetter = () => viewModel.Model?.MutedUsers;
        UsersSetter = (value) => viewModel.Model!.MutedUsers = value;
    }

    protected override Func<RangeObservableCollection<UserModel>?> UsersGetter { get; init; }
    protected override Action<RangeObservableCollection<UserModel>?> UsersSetter { get; init; }
}
