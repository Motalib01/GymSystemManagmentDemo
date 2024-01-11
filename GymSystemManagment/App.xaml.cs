using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace GymSystemManagment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        
            protected override void OnStartup(StartupEventArgs e)
            {
            base.OnStartup(e);

            // Show splash screen
            Splach splashScreen = new Splach();
            splashScreen.Show();

            // Initialize and show the main window
            MainWindow mainWindow = new MainWindow();

            // Optionally add a delay or initialization logic here

            // Close the splash screen
            splashScreen.Close();

            // Show the main window
            mainWindow.Show();
        }
        }
    
}
