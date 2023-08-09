using System.Windows.Input;

namespace NativeApp.Interfaces;

public interface INavigateCommandFactory
{
    public ICommand Create(string key, object value, string route);
    public ICommand Create(string key, object value, string route, Action beforeNavigation);
    public ICommand Create(string key, string route, Func<object> getValue);
    public ICommand Create(string key, string route, Func<object, object> getValue);
    public ICommand Create(string key, string route, Func<object, IDictionary<string, object>> getValues);
}
