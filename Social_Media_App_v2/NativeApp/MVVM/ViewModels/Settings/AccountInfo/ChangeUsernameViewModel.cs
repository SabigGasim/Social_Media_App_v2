using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.Validations.Rules;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.AccountInfo;
public class ChangeUsernameViewModel : SubSettingsViewModelBase<AccountInfoViewModel, AccountInfoModel>
{
    private ICommand? _textChangedCommand;

    public ChangeUsernameViewModel(
        INavigationService navigationService, 
        AccountInfoViewModel accountInfoViewModel) : base(navigationService, accountInfoViewModel)
    {
        AddValidations();
    }

    public ValidatableObject<string> Username { get; private set; } = new();

    public void SetOriginalUsername()
    {
        Username.Value = this.Model!.Username;
    }
    
    public ICommand? TextChangedCommand => _textChangedCommand ??= new Command(() =>
    {
        Username.Validate();
    });

    protected override Task<Result> UpdateSettings()
    {
        if (!Username.IsValid)
        {
            return Task.FromResult(Results.Fail(new ValidationException("Username is invalid")));
        }

        //1 - Copy the old value of the AccountInfo
        //2 - Do the required updates
        //3 - Send an UPDATE reuqest to the server
        //4 - if succeded, update the viewModel.

        var accountInfo = GetModifiedAccountInfo();

        SetAccountInfoOnMainThread(accountInfo);

        return base.UpdateSettings();
    }

    private AccountInfoModel GetModifiedAccountInfo()
    {
        var accountInfo = Model!.DeepCopy();
        accountInfo.Username = Username.Value!;
        return accountInfo;
    }

    private void AddValidations()
    {
        Username.Validations!.AddRange(new IValidationRule<string>[]
        {
            new IsNotNullOrEmptyRule<string>() { PropertyName = nameof(Username) },
            new UsernameRule<string>() { PropertyName = nameof(Username) },
            new IsInLengthRule<string>(1, 15) { PropertyName = nameof(Username) }
        });
    }

    private void SetAccountInfoOnMainThread(AccountInfoModel accountInfo)
    {
        if(MainThread.IsMainThread)
        {
            base.Model = accountInfo;
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => base.Model = accountInfo);
    }
}
