using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public class AccountAlertViewModel : AlertViewModelBase<AccountAlertModel>
{
    private AccountAlertModel? _accountAlertModel;
    private ICommand? _alertClickedCommand;

    public override AccountAlertModel? Alert 
    { 
        get => _accountAlertModel;
        set => TrySetValue(ref _accountAlertModel, value); 
    }

    public override ICommand? AlertClickedCommand => _alertClickedCommand;
}
