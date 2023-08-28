using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeUsernamePage : ContentPage
{
    private readonly ChangeUsernameViewModel _viewModel;

    public ChangeUsernamePage(ChangeUsernameViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.SetOriginalUsername();

        base.OnNavigatedTo(args);
    }
}