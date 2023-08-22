using CommunityToolkit.Maui.Views;
using System.Windows.Input;

namespace NativeApp.Interfaces;

public interface INavigateCommandFactory
{
    /// <summary>
    /// Navigates to a page with route being passed through a command parameter
    /// </summary>
    public ICommand Create();
    public ICommand Create(string route);
    public ICommand Create<TPopup>(Func<TPopup> popup) where TPopup : Popup;
    public ICommand Create<TPopup, TResult>(Func<TPopup> popup, Action<TResult> action) where TPopup : Popup;
    public ICommand Create<TPopup, TResult>(Func<TPopup> popup, Func<TResult, Task> task) where TPopup : Popup;
    public ICommand Create(string key, object value, string route, Action? afterNavigation = null);
    public ICommand Create(string key, string route);
    public ICommand Create(string key, string route, Func<object> getValue);
    public ICommand Create(string key, string route, Func<object, object> getValue);
    public ICommand Create(string key, string route, Func<object, IDictionary<string, object>> getValues);
}
