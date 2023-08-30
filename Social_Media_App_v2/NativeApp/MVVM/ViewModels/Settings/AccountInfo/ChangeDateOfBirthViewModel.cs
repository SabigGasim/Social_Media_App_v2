using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.Validations.Rules;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings.AccountInfo;
public class ChangeDateOfBirthViewModel : SubSettingsViewModelBase<AccountInfoViewModel, AccountInfoModel>
{
    private ICommand? _dateSelectedCommand;

    public ChangeDateOfBirthViewModel(
        INavigationService navigationService,
        AccountInfoViewModel accountInfoViewModel) : base(navigationService, accountInfoViewModel)
    {
        AddValidations();
    }

    public ValidatableObject<DateOnly> DateOfBirth { get; private set; } = new();

    public void SetOriginalDateOfBirth()
    {
        DateOfBirth.Value = this.Model!.DateOfBirth;
    }

    public ICommand? DateSelectedCommand => _dateSelectedCommand ??= new Command(() => DateOfBirth.Validate());

    protected override Task<Result> UpdateSettings()
    {
        if (!DateOfBirth.IsValid)
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
        accountInfo.DateOfBirth = DateOfBirth.Value!;
        return accountInfo;
    }

    private void AddValidations()
    {
        DateOfBirth.Validations!.AddRange(new IValidationRule<DateOnly>[]
        {
            new IsNotNullOrEmptyRule<DateOnly>() { PropertyName = nameof(DateOfBirth) },
            new OlderThanRule<DateOnly>(11) { PropertyName = "Age" } //this PropertyName fits the ValidationMessage better
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
