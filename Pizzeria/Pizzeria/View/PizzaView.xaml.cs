using Pizzeria.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for PizzaView.xaml
    /// </summary>
    public partial class PizzaView : UserControl
    {
        public PizzaView()
        {
            InitializeComponent();
            DataContext = new PizzaViewModel();
        }

        private void ValidateDecimalInput(object sender, TextCompositionEventArgs e)
        {
            // Regex die zowel komma's als punten accepteert
            Regex regex = new Regex(@"^[0-9]*(?:[.,][0-9]*)?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void FormatDecimalInput(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox)
            {
                string text = textBox.Text.Replace(',', '.'); // Zet komma's om naar punten
                if (decimal.TryParse(text, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal result))
                {
                    textBox.Text = result.ToString("F2", System.Globalization.CultureInfo.InvariantCulture); // Formatteer naar 2 decimalen
                }
                else
                {
                    textBox.Text = "0.00"; // Standaardwaarde als de invoer ongeldig is
                }
            }
        }

    }
}
