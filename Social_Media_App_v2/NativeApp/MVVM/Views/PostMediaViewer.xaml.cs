using NativeApp.MVVM.Models;

namespace NativeApp.MVVM.Views;

public partial class PostMediaViewer : ContentPage, IQueryAttributable
{
	public PostMediaViewer()
	{
		InitializeComponent();
	}

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
		var media = (MediaListModel)query[nameof(MediaListModel)];
		var index = (int)query["Index"];

		BindingContext = media;

		PostImagesCarouselView.Position = index;
    }
}