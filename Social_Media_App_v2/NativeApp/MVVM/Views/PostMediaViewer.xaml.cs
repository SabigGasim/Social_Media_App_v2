using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels;

namespace NativeApp.MVVM.Views;

public partial class PostMediaViewer : ContentPage, IQueryAttributable
{
	public PostMediaViewer()
	{
		InitializeComponent();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
		var post = (PostModel)query[nameof(PostModel)];
		var index = (int)query["Index"];

		BindingContext = post;

		PostImagesCarouselView.Position = index;
    }
}