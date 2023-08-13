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
}
