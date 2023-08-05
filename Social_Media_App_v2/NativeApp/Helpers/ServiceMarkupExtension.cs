namespace NativeApp.Helpers;

public class ServiceMarkupExtension : IMarkupExtension<object>
{
    public Type Type { get; set; }

    public object ProvideValue(IServiceProvider serviceProvider)
    {
        var service = serviceProvider?.GetRequiredService(Type);

        return service;
    }

    object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
    {
        return ProvideValue(serviceProvider);
    }
}