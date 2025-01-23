using Pizzeria.Helpers;
using Pizzeria.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;



namespace Pizzeria.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Properties

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowContactPageCommand { get; }
        public ICommand ShowLoginPageCommand { get; }
        public ICommand ShowPizzaPageCommand { get; }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            ShowContactPageCommand = new RelayCommand(ExecuteShowContactPage);
            ShowLoginPageCommand = new RelayCommand(ExecuteShowLoginPage);
            ShowPizzaPageCommand = new RelayCommand(ExecuteShowPizzaPage);

            // Default View
            ExecuteShowContactPage();
        }

        #endregion

        #region Methods

        private void ExecuteShowContactPage(object obj = null)
        {
            CurrentView = new ContactPageViewModel();
        }

        private void ExecuteShowLoginPage(object obj = null)
        {
            CurrentView = new LogInViewModel();
        }

        private void ExecuteShowPizzaPage(object obj = null)
        {
            CurrentView = new PizzaViewModel();
        }

        #endregion
    }
}
