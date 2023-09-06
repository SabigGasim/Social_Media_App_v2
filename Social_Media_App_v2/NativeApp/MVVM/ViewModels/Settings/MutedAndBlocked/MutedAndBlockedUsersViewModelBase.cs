using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using System.ComponentModel;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

public abstract class MutedAndBlockedUsersViewModelBase<TViewModel> : ViewModelBase
{
    private readonly Func<RangeObservableCollection<UserModel>?> _usersGetter;
    private readonly Action<RangeObservableCollection<UserModel>?> _usersSetter;
    private readonly Subscriber _mutedUsersSubscriber;
    private readonly IServiceProvider _serviceProvider;
    private readonly PropertyChangedEventHandler OnMutedUsersChanged;

    public MutedAndBlockedUsersViewModelBase(
        MutedAndBlockedViewModel viewModel,
        IServiceProvider serviceProvider,
        Func<RangeObservableCollection<UserModel>?> usersGetter,
        Action<RangeObservableCollection<UserModel>?> usersSetter)
    {
        _usersGetter = usersGetter;
        _usersSetter = usersSetter;
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

        if (viewModels is null)
        {
            return (RangeObservableCollection<UserViewModel>?)Enumerable.Empty<UserViewModel>();
        }

        return new RangeObservableCollection<UserViewModel>(viewModels);
    }

    private void SubscribeToAccountInfo() => _mutedUsersSubscriber.Subscribe(OnMutedUsersChanged);
}

