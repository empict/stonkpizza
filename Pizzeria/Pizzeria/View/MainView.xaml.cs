using Pizzeria.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Pizzeria.View
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            DataContext = new ViewModel.MainViewModel();
        }

       

        private void ShowContactPage(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new ContactPage(); // Laadt ContactPage UserControl
        }


        private void ShowLoginPage(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new LogInView();
        }

        private void ShowPizzaPage(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new PizzaView(); // Laadt de PizzaView UserControl
        }

        private void ShowBestellingenPage(object sender, RoutedEventArgs e)
        {
            ContentArea.Content = new BestellingViewModel();
        }





    }
}
