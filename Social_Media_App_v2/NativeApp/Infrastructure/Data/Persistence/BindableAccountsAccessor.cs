using NativeApp.Helpers;
using NativeApp.Interfaces;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.Infrastructure.Data.Persistence;

public class BindableAccountsAccessor : ViewModelBase, IAccountsAccessor
{
    private readonly AccountsAccessor _accountsAccessor;

    public BindableAccountsAccessor(AccountsAccessor accountsAccessor)
    {
        _accountsAccessor = accountsAccessor;
        Accounts ??= new();
    }

    public UserViewModel? CurrentAccount 
    {
        get => _accountsAccessor.CurrentAccount; 
        set
        {
            if(_accountsAccessor.CurrentAccount != value)
            {
                _accountsAccessor.CurrentAccount = value;
                OnPropertyChanged();
            }
        }
    }

    public RangeObservableCollection<UserViewModel>? Accounts
    {
        get => _accountsAccessor.Accounts;
        set
        {
            if (_accountsAccessor.Accounts != value)
            {
                _accountsAccessor.Accounts = value;
                OnPropertyChanged();
            }
        }
    }
}
