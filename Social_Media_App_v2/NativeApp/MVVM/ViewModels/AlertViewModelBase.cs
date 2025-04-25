using NativeApp.Helpers;
using NativeApp.MVVM.Models;
using System.Windows.Input;

namespace NativeApp.MVVM.ViewModels;

public abstract class AlertViewModelBase : ViewModelBase
{
    public abstract ICommand? AlertClickedCommand { get; }
}

public abstract class AlertViewModelBase<TAlertModel> : AlertViewModelBase where TAlertModel : AlertModelBase
{
    public abstract TAlertModel? Alert { get; set; }
}
