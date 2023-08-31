using Domain.Common;
using NativeApp.Interfaces;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels.Settings;
public abstract class SubSettingsViewModelBase<TViewModel, TModel> : ViewModelBase, IDisposable
    where TViewModel : SettingsViewModelBase<TModel>
{
    private readonly INavigationService _navigationService;
    private ICommand? _saveSettingsCommand;
    private Func<TModel?> _modelGetter;
    private Action<TModel?> _modelSetter;

    public SubSettingsViewModelBase(INavigationService navigationService, TViewModel viewModel)
    {
        _navigationService = navigationService;

        //only delegate access to the model's getter and setter and not the whole view model.
        //delegates are used here so the NotifiyPropertyChanged event of the view model's model gets triggered.
        _modelGetter = () => viewModel.Model;
        _modelSetter = (value) => viewModel.Model = value;
    }

    protected TModel? Model
    {
        get => _modelGetter();
        set
        {
            _modelSetter(value);
            OnPropertyChanged(nameof(Model));
        }
    }

    public virtual ICommand? SaveSettingsCommand => _saveSettingsCommand ??=
        new Command(async () =>
        {
            var result = await UpdateSettings();
            if (result.Success)
            {
                return;
            }

            HandleErrorResult(result);
        });

    protected virtual async Task<Result> UpdateSettings()
    {
        await _navigationService.PopAsync();
        return Results.Success();
    }

    protected virtual void HandleErrorResult(Result result)
    {
#if DEBUG
        var errorMessage = string.Join('\n', result.Errors!.Select(e => e.Message));

        Shell.Current.CurrentPage.Dispatcher
        .Dispatch(() => Shell.Current
        .DisplayAlert("Error", errorMessage, "Cancel"));
#endif
    }

    public void Dispose()
    {
        _modelGetter -= _modelGetter;
        _modelSetter -= _modelSetter;
    }
}
