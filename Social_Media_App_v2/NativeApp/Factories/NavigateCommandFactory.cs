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

    public ICommand Create<TParameter>(string key, string route) where TParameter : class
    {
        return new Command(async (parameter) =>
        {
            var param = _routeParametersFactory.Create(key, parameter as TParameter);
            await _navigationService.NavigateToAsync(route, param);
        });
    }

    public ICommand Create(string key, object value, string route, Action beforeNavigation)
    {
        return new Command(async () =>
        {
            beforeNavigation();

            var param = _routeParametersFactory.Create(key, value);
            await _navigationService.NavigateToAsync(route, param);
        });
    }
}
