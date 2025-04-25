using CommunityToolkit.Maui.Views;

namespace NativeApp.Interfaces;
public interface INavigationService
{
    Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null);
    Task<object?> ShowPopupAsync<TPopup>(TPopup popup) where TPopup : Popup;

    Task PopAsync(string route = "..");
}
