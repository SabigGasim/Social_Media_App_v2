using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using System.ComponentModel;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
public class MutedAccountsViewModel : ViewModelBase
{
    private readonly Func<RangeObservableCollection<UserModel>?> _usersGetter;
    private readonly Action<RangeObservableCollection<UserModel>?> _usersSetter;
    private readonly Subscriber _mutedUsersSubscriber;
    private readonly IServiceProvider _serviceProvider;
    private readonly PropertyChangedEventHandler OnMutedUsersChanged;
    private ICommand? _unMuteUserCommand;

    public MutedAccountsViewModel(
        MutedAndBlockedViewModel viewModel,
        IServiceProvider serviceProvider)
    {
        _usersGetter = () => viewModel.Model!.MutedUsers;
        _usersSetter = (value) => viewModel.Model!.MutedUsers = value;
        _mutedUsersSubscriber = viewModel.Subscriber;
        _serviceProvider = serviceProvider;

        OnMutedUsersChanged = (sender, args) =>
        {
            if (args.PropertyName == nameof(MutedAndBlockedViewModel.Model))
            {
                OnPropertyChanged(nameof(UserViewModels));
            }
        };

        SubscribeToAccountInfo();
    }

    public RangeObservableCollection<UserViewModel>? UserViewModels 
    {
        get => MapToUserViewModels(_usersGetter());
        set
        {
            _usersSetter(new(value?.Select(x => x.User)!));
            OnPropertyChanged(nameof(UserViewModels));
        }
    }

    private RangeObservableCollection<UserViewModel>? MapToUserViewModels(IEnumerable<UserModel>? userModels)
    {
        var viewModels = userModels?.Select(x =>
        {
            var vm = _serviceProvider.GetRequiredService<UserViewModel>();
            vm.User = x;
            return vm;
        });

        if(viewModels is null)
        {
            return (RangeObservableCollection<UserViewModel>?)Enumerable.Empty<UserViewModel>();
        }

        return new RangeObservableCollection<UserViewModel>(viewModels);
    }

    private void SubscribeToAccountInfo() => _mutedUsersSubscriber.Subscribe(OnMutedUsersChanged);
}
