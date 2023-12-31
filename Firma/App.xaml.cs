﻿using Firma.ViewModels;
using System.Windows;

namespace Firma
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            base.OnStartup(e);
            MainWindow window = new MainWindow();
            var viewModel = new MainWindowViewModel();
            window.DataContext = viewModel; 
            window.Show();
        }

    }
}
