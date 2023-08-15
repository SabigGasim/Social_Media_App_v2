namespace NativeApp.MVVM.Models;

public abstract class AlertModelBase
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
}
