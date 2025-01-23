using Pizzeria.Helpers;
using Pizzeria.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using System.Windows;


namespace Pizzeria.ViewModel
{
    public class BestellingViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private Bestelling _selectedBestelling;

        public ObservableCollection<Bestelling> Bestellingen { get; set; }
        public ObservableCollection<Status> Statussen { get; set; }

        public Bestelling SelectedBestelling
        {
            get => _selectedBestelling;
            set
            {
                _selectedBestelling = value;
                OnPropertyChanged();
                ((RelayCommand)UpdateStatusCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand UpdateStatusCommand { get; }

        public BestellingViewModel()
        {
            _context = new AppDbContext();

            // Laad bestellingen en statussen
            Bestellingen = new ObservableCollection<Bestelling>(_context.Bestelling.Include(b => b.Status).ToList());
            Statussen = new ObservableCollection<Status>(_context.Status.ToList());

            // Initializeer het commando
            UpdateStatusCommand = new RelayCommand(ExecuteUpdateStatus, CanExecuteUpdateStatus);
        }

        private void ExecuteUpdateStatus(object obj)
        {
            try
            {
                if (SelectedBestelling == null)
                {
                    MessageBox.Show("Selecteer een bestelling om de status bij te werken.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Haal de geselecteerde bestelling op
                var bestelling = _context.Bestelling.FirstOrDefault(b => b.Id == SelectedBestelling.Id);
                if (bestelling != null)
                {
                    // Update de status
                    bestelling.status_id = SelectedBestelling.Status.id;
                    _context.Bestelling.Update(bestelling);
                    _context.SaveChanges();

                    // Refresh de lijst
                    Bestellingen = new ObservableCollection<Bestelling>(_context.Bestelling.Include(b => b.Status).ToList());
                    OnPropertyChanged(nameof(Bestellingen));

                    MessageBox.Show("Status succesvol bijgewerkt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("De geselecteerde bestelling is niet gevonden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "bijwerken van de status");
            }
        }

        private bool CanExecuteUpdateStatus(object obj)
        {
            return SelectedBestelling != null && SelectedBestelling.Status != null;
        }

        private void ShowErrorMessage(Exception ex, string action)
        {
            string errorMessage = ex.Message;
            if (ex.InnerException != null)
            {
                errorMessage += "\nInner Exception: " + ex.InnerException.Message;
            }

            MessageBox.Show($"Er is een fout opgetreden bij het {action}: {errorMessage}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

}
