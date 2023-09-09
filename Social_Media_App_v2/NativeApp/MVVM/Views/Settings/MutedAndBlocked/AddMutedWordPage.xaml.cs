using NativeApp.MVVM.ViewModels.Settings.MutedAndBlocked;

namespace NativeApp.MVVM.Views.Settings.MutedAndBlocked;

public partial class AddMutedWordPage : ContentPage
{
	public AddMutedWordPage(AddMutedWordViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
    }
}