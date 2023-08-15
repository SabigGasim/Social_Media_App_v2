using Infrastructure.Interfaces;
using NativeApp.Helpers;
using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.ViewModels;

public class AlertsViewModel : ViewModelBase
{
    private RangeObservableCollection<AlertViewModelBase>? _alerts = new();
    private readonly IAlertsRepository _repository;
    private readonly IServiceProvider _serviceProvider;

    public AlertsViewModel(
        IAlertsRepository repository,
        IServiceProvider serviceProvider)
    {
        _repository = repository;
        _serviceProvider = serviceProvider;
    }

    public async Task UpdateAlerts(Guid? lastSeenAlertId, int numberOfAlerts)
    {
        var result = await _repository.GetAlerts(Guid.NewGuid(), lastSeenAlertId, numberOfAlerts);
        
        if (result.Success)
        {
            var alerts = result.Value!.Map().Select(GetAlertViewModel).ToList();
            Alerts!.AddRange(alerts);
        }
    }

    private AlertViewModelBase GetAlertViewModel(AlertModelBase alert)
    {
        if (alert is UserAlertModel userAlert)
        {
            var userAlertViewModel = _serviceProvider.GetRequiredService<UserAlertViewModel>();
            userAlertViewModel.Alert = userAlert;
            return userAlertViewModel;
        }

        var accountAlert = (AccountAlertModel)alert;
        var accountAlertViewModel = _serviceProvider.GetRequiredService<AccountAlertViewModel>();
        accountAlertViewModel.Alert = accountAlert;

        return accountAlertViewModel;
    }

    public RangeObservableCollection<AlertViewModelBase>? Alerts
    {
        get => _alerts;
        set => TrySetValue(ref _alerts, value);
    }
}
