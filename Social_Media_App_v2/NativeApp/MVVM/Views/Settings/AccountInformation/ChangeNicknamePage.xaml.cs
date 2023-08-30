using NativeApp.MVVM.ViewModels.Settings.AccountInfo;

namespace NativeApp.MVVM.Views.Settings.AccountInformation;

public partial class ChangeNicknamePage : ContentPage
{
    private readonly ChangeNicknameViewModel _viewModel;

    public ChangeNicknamePage(ChangeNicknameViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        _viewModel.SetOriginalNickname();

        base.OnNavigatedTo(args);
    }
}