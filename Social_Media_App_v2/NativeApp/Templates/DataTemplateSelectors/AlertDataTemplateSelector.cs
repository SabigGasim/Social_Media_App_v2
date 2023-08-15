using NativeApp.MVVM.ViewModels;

namespace NativeApp.Templates.DataTemplateSelectors;

public class AlertDataTemplateSelector : DataTemplateSelector
{
    public DataTemplate AccountAlertDataTemplate { get; set; } = default!;
    public DataTemplate UserAlertDataTemplate { get; set; } = default!;

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        return item is UserAlertViewModel
            ? UserAlertDataTemplate
            : AccountAlertDataTemplate;
    }
}
