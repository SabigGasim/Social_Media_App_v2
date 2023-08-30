using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeDateOfBirthPage : ContentPage
{
    private ChangeDateOfBirthViewModel _viewModel;

    public ChangeDateOfBirthPage(ChangeDateOfBirthViewModel viewModel)
	{
		InitializeComponent();

		_viewModel = viewModel;
		BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.SetOriginalDateOfBirth();

        base.OnNavigatedTo(args);
    }
}