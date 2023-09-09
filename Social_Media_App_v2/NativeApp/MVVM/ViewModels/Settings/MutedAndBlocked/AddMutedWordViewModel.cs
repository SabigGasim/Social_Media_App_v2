using NativeApp.MVVM.Models;
using Domain.Common;
using NativeApp.Interfaces;
using NativeApp.Validations.Rules;
using System.ComponentModel.DataAnnotations;
using Domain.Enums;
using Domain.Extensions;
using System.Windows.Input;
using Humanizer;
using System.Collections.Immutable;

namespace NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;
public class AddMutedWordViewModel : SubSettingsViewModelBase<MutedAndBlockedViewModel, MutedAndBlockedModel>
{
    private ICommand? _textChangedCommand;
    private ICommand? _durationChangedCommand;

    public AddMutedWordViewModel(
        INavigationService navigationService,
        MutedAndBlockedViewModel viewModel) : base(navigationService, viewModel)
    {   
        AddValidations();

        DurationNames = Enum.GetNames<Duration>()
            .Select(x => x.Humanize(LetterCasing.Sentence))
            .ToImmutableList();

        SetOriginalSettings();

        ValidateViewModel();
    }

    public ValidatableObject<string> Word { get; private set; } = new();
    public ValidatableObject<Duration> Duration { get; set; } = new();
    public ImmutableList<string> DurationNames { get; private set; } 

    public ICommand? TextChangedCommand => _textChangedCommand ??= new Command(() => Word.Validate());
    public ICommand? DurationChangedCommand => _durationChangedCommand ??= new Command(() => Duration.Validate()); 

    protected override void SetOriginalSettings()
    {
        Word.Value = "";
        Duration.Value = Domain.Enums.Duration.Unspecified;
    }

    protected override Task<Result> UpdateSettings()
    {
        if (!ViewModelIsValid())
        {
            return Task.FromResult(Results.Fail(new ValidationException("View model is invalid")));
        }

        AddWordOnMainThread(new(Word.Value!, Duration.Value.ToDateTimeOffset()));

        return base.UpdateSettings();
    }

    private void AddValidations()
    {
        Word.Validations!.AddRange(new IValidationRule<string>[]
        {
            new IsNotNullOrEmptyRule<string>() { PropertyName = nameof(Word) },
            new IsNotWhiteSpaceRule<string>() { PropertyName = nameof(Word) }
        });

        Duration.Validations!.Add(new IsNotEqualToRule<Duration>(Domain.Enums.Duration.Unspecified));
    }

    private void AddWordOnMainThread(MutedWordModel model)
    {
        if (MainThread.IsMainThread)
        {
            AddWord(model);
            return;
        }

        MainThread.BeginInvokeOnMainThread(() => AddWord(model));
    }

    private void AddWord(MutedWordModel model)
    {
        Model!.MutedWords!.Add(model);
        SetOriginalSettings();
    }

    private bool ViewModelIsValid()
    {
        ValidateViewModel();

        return Word.IsValid && Duration.IsValid;
    }

    private void ValidateViewModel()
    {
        Word.Validate();
        Duration.Validate();
    }
}
