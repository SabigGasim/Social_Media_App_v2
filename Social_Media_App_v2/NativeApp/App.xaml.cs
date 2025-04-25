﻿using NativeApp.MVVM.ViewModels;
using System.Globalization;

namespace NativeApp;

public partial class App : Application
{
    public App(ShellViewModel viewModel)
    {
        InitializeComponent();

        CultureInfo.CurrentCulture = new CultureInfo("en-US");
        CultureInfo.CurrentUICulture = new CultureInfo("en-US");

        MainPage = new AppShell(viewModel);
    }
}
