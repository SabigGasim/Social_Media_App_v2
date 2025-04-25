using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class SearchPage : ContentPage
{
    private UserSearchViewModel _viewModel;

    public SearchPage(UserSearchViewModel viewModel)
	{
		InitializeComponent();

        _viewModel = viewModel;

        BindingContext = viewModel;
    }

    private void SearchTextChanged(object sender, TextChangedEventArgs e)
    {
        _viewModel.PerformSearchCommand!.Execute(e.NewTextValue);
    }
}