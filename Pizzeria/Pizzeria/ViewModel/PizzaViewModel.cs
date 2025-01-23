using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Pizzeria.Model;
using Pizzeria.Helpers;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace Pizzeria.ViewModel
{
    public class PizzaViewModel : ObservableObject
    {
        private readonly AppDbContext _context;
        private Pizza _selectedPizza;

        public ObservableCollection<Pizza> Pizzas { get; set; }
        public Pizza NewPizza { get; set; } = new Pizza();

        public Pizza SelectedPizza
        {
            get => _selectedPizza;
            set
            {
                _selectedPizza = value;
                OnPropertyChanged();
                ((RelayCommand)UpdatePizzaCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeletePizzaCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand CreatePizzaCommand { get; }
        public ICommand UpdatePizzaCommand { get; }
        public ICommand DeletePizzaCommand { get; }
        public ICommand SelectImageCommand { get; }

        public PizzaViewModel()
        {
            _context = new AppDbContext();

            // Load existing pizzas from the database
            Pizzas = new ObservableCollection<Pizza>(_context.Pizzas.ToList());

            // Initialize commands
            CreatePizzaCommand = new RelayCommand(ExecuteCreatePizza);
            UpdatePizzaCommand = new RelayCommand(ExecuteUpdatePizza, CanExecuteUpdateOrDelete);
            DeletePizzaCommand = new RelayCommand(ExecuteDeletePizza, CanExecuteUpdateOrDelete);
            SelectImageCommand = new RelayCommand(ExecuteSelectImage);
        }

        private void ExecuteCreatePizza(object obj)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(NewPizza.Naam))
                {
                    MessageBox.Show("Naam is verplicht.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (NewPizza.Prijs <= 0)
                {
                    MessageBox.Show("Prijs moet groter zijn dan 0.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _context.Pizzas.Add(NewPizza);
                _context.SaveChanges();

                Pizzas.Add(NewPizza);

                NewPizza = new Pizza();
                OnPropertyChanged(nameof(NewPizza));

                MessageBox.Show("Pizza succesvol toegevoegd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "toevoegen van de pizza");
            }
        }

        private void ExecuteUpdatePizza(object obj)
        {
            try
            {
                if (SelectedPizza == null)
                {
                    MessageBox.Show("Selecteer een pizza om te bewerken.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _context.Pizzas.Update(SelectedPizza);
                _context.SaveChanges();

                MessageBox.Show("Pizza succesvol bijgewerkt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "bijwerken van de pizza");
            }
        }

        private void ExecuteDeletePizza(object obj)
        {
            try
            {
                if (SelectedPizza == null)
                {
                    MessageBox.Show("Selecteer een pizza om te verwijderen.", "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                _context.Pizzas.Remove(SelectedPizza);
                _context.SaveChanges();

                Pizzas.Remove(SelectedPizza);

                MessageBox.Show("Pizza succesvol verwijderd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "verwijderen van de pizza");
            }
        }

        private void ExecuteSelectImage(object obj)
        {
            try
            {
                var openFileDialog = new OpenFileDialog
                {
                    Filter = "Afbeeldingsbestanden (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png",
                    Title = "Selecteer een afbeelding"
                };

                if (openFileDialog.ShowDialog() == true)
                {
                    NewPizza.Image = File.ReadAllBytes(openFileDialog.FileName);
                    OnPropertyChanged(nameof(NewPizza));
                    MessageBox.Show("Afbeelding succesvol geselecteerd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex, "selecteren van de afbeelding");
            }
        }

        private bool CanExecuteUpdateOrDelete(object obj)
        {
            return SelectedPizza != null;
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
