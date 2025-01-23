using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pizzeria.Helpers;
using Pizzeria.Model;
using System.Windows.Input;
using BCrypt.Net;
using System.Diagnostics; // Voor wachtwoordhashing
using System.Security.Cryptography;

namespace Pizzeria.ViewModel
{
    public class LogInViewModel : ObservableObject
    {
        private readonly AppDbContext _context;

        public string Gebruikersnaam { get; set; } = string.Empty;
        public string Wachtwoord { get; set; } = string.Empty;
        public string Foutmelding { get; set; } = string.Empty;

        public ICommand LogInCommand { get; }

        public LogInViewModel()
        {
            _context = new AppDbContext();
            LogInCommand = new RelayCommand(LogIn, CanLogIn);
        }

        private bool CanLogIn(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Gebruikersnaam) && !string.IsNullOrWhiteSpace(Wachtwoord);
        }

        private void LogIn(object? parameter)
        {
            try
            {
                // Hash het ingevoerde wachtwoord
                var gehashteWachtwoord = Convert.ToBase64String(
                    System.Security.Cryptography.SHA256.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(Wachtwoord)
                    ));

                // Zoek de gebruiker in de database
                var gebruiker = _context.Medewerkers.FirstOrDefault(m =>
                    m.Naam == Gebruikersnaam && m.Wachtwoord == gehashteWachtwoord);

                if (gebruiker != null)
                {
                    if (gebruiker.FunctieId == 1) // Manager
                    {
                        Foutmelding = "Welkom, Manager!";
                        // Open de view voor het aanmaken van medewerkers
                    }
                    else
                    {
                        Foutmelding = "Geen rechten om medewerkers aan te maken.";
                    }
                }
                else
                {
                    Foutmelding = "Ongeldige inloggegevens!";
                }
            }
            catch (Exception ex)
            {
                Foutmelding = $"Fout bij inloggen: {ex.Message}";
            }
        }
    }

}
