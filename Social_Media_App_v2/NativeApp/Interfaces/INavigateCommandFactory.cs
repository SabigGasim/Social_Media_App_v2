using System.Windows.Input;

namespace NativeApp.Interfaces;

public interface INavigateCommandFactory
{
    public ICommand Create(string key, object value, string route);
    public ICommand Create(string key, object value, string route, Action beforeNavigation);
    public ICommand Create<TParameter>(string key, string route) where TParameter : class;
}
