using CommunityToolkit.Maui.Views;
using NativeApp.Interfaces;
using System.Windows.Input;

namespace NativeApp.Factories;

public class NavigateCommandFactory : INavigateCommandFactory
{
    private readonly INavigationService _navigationService;
    private readonly IRouteParametersFactory _routeParametersFactory;

    public NavigateCommandFactory(
        INavigationService navigationService,
        IRouteParametersFactory routeParametersFactory)
    {
        _navigationService = navigationService;
        _routeParametersFactory = routeParametersFactory;
    }

    public ICommand Create(string key, object value, string route)
    {
        return new Command(async () =>
        {
            var param = _routeParametersFactory.Create(key, value);
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string key, string route, Func<object> getValue)
    {
        return new Command(async () =>
        {
            var param = _routeParametersFactory.Create(key, getValue());
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string key, string route, Func<object, object> getValue)
    {
        return new Command(async (object context) =>
        {
            var param = _routeParametersFactory.Create(key, getValue(context));
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string key, string route, Func<object, IDictionary<string, object>> getValues)
    {
        return new Command(async (object context) =>
        {
            var param = getValues(context);
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string key, string route)
    {
        return new Command(async (object commandParameter) =>
        {
            var param = _routeParametersFactory.Create(key, commandParameter);
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string route)
    {
        return new Command(async () =>
        {
            await _navigationService.NavigateToAsync(route);
        });
    }

    public ICommand Create<TPopup>(Func<TPopup> popup) where TPopup : Popup
    {
        return new Command(async () =>
        {
            await _navigationService.ShowPopupAsync(popup());
        });
    }

    public ICommand Create<TPopup, TResult>(Func<TPopup> popup, Action<TResult> action) where TPopup : Popup
    {
        return new Command(async () =>
        {
            var result = await _navigationService.ShowPopupAsync(popup());
            if(result is TResult obj && obj is not null)
            {
                action(obj);
            }
        });
    }

    public ICommand Create<TPopup, TResult>(Func<TPopup> popup, Func<TResult, Task> task) where TPopup : Popup
    {
        return new Command(async () =>
        {
            var result = await _navigationService.ShowPopupAsync(popup());
            if (result is TResult obj && obj is not null)
            {
                await task(obj);
            }
        });
    }
}
