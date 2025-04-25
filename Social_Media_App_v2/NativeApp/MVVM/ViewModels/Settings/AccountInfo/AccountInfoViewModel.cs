using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.AccountInfo;
public class AccountInfoViewModel : SettingsViewModelBase<AccountInfoModel>
{
    private readonly INavigateCommandFactory _navigateCommandFactory;
    private ICommand? _tableCellClickedCommand;
    private AccountInfoModel? _accountInfo;

    public AccountInfoViewModel(INavigateCommandFactory navigateCommandFactory)
    {
        _navigateCommandFactory = navigateCommandFactory;
    }

    public ICommand? TableCellClickedCommand => _tableCellClickedCommand ??= _navigateCommandFactory.Create();

    public override AccountInfoModel? Model 
    {
        get => _accountInfo;
        set => TrySetValue(ref _accountInfo, value); 
    }
}
