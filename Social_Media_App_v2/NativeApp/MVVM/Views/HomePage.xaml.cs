using Microsoft.Maui.Animations;

using NativeApp.Interfaces;
using NativeApp.MVVM.Models;
using NativeApp.MVVM.ViewModels;
using NativeApp.Templates.DataTemplateSelectors;
using System.Collections.ObjectModel;

namespace NativeApp.MVVM.Views;

public partial class HomePage : ContentPage
{
	public HomePage(
		TimelineViewModel viewModel)
	{
		InitializeComponent();

		viewModel.UpdateTimeline(null).GetAwaiter().GetResult();

        BindingContext = viewModel;
	}

	private void InitializeDataTemplates()
	{

	}
}