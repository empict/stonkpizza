using System.Configuration;
using System.Data;
using System.Windows;
using Pizzeria.View;
using Pizzeria.ViewModel;

namespace Pizzeria
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Maak een Window aan dat MainView als inhoud bevat
            var mainWindow = new Window
            {
                Content = new MainView(),
                Title = "Pizeria Applicatie",
                Height = 600,
                Width = 900
            };

            mainWindow.Show();
        }
    }

}
