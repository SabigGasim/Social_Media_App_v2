using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class ProfilePage : ContentPage, IQueryAttributable
{
    private readonly ProfileViewModel _viewModel;

    public ProfilePage(ProfileViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (!query.Any())
        {
            return;
        }

        var userModel = (UserModel)query[nameof(UserModel)];
        
        _viewModel.UserViewModel!.User = userModel;
        _viewModel.UserViewModel!.IsDisplayingUserProfile = true;

        await _viewModel.UpdatePosts(null, 20);

        BindingContext = _viewModel;

        query.Clear();
    }
}