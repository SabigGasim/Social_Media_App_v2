using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.Validations.Rules;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.AccountInfo;
public class ChangeNicknameViewModel : SubSettingsViewModelBase<AccountInfoViewModel, AccountInfoModel>
{
    private ICommand? _textChangedCommand;

    public ChangeNicknameViewModel(
        INavigationService navigationService,
        AccountInfoViewModel accountInfoViewModel) : base(navigationService, accountInfoViewModel)
    {
        AddValidations();
    }

    public ValidatableObject<string> Nickname { get; private set; } = new();

    public void SetOriginalNickname()
    {
        Nickname.Value = this.Model!.Nickname;
    }

    public ICommand? TextChangedCommand => _textChangedCommand ??= new Command(() => Nickname.Validate());

    protected override Task<Result> UpdateSettings()
    {
        if (!Nickname.IsValid)
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
        accountInfo.Nickname = Nickname.Value!;
        return accountInfo;
    }

    private void AddValidations()
    {
        Nickname.Validations!.AddRange(new IValidationRule<string>[]
        {
            new IsNotNullOrEmptyRule<string>() { PropertyName = nameof(Nickname) },
            new IsNotWhiteSpaceRule<string>() { PropertyName = nameof(Nickname) },
            new IsInLengthRule<string>(1, 15) { PropertyName = nameof(Nickname) },
            new NicknameRule<string>() { PropertyName = nameof(Nickname) },
        }); 
    }

    private void SetAccountInfoOnMainThread(AccountInfoModel accountInfo)
    {
        if (MainThread.IsMainThread)
        {
            base.Model = accountInfo;
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => base.Model = accountInfo);
    }
}
