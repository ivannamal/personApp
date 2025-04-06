using PersonApp.Exceptions;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PersonApp
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDate;

        private RelayCommand _proceedCommand;
        public ICommand ProceedCommand => _proceedCommand;

        public MainViewModel()
        {
            _proceedCommand = new RelayCommand(Proceed, () => CanProceed);
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
                RaiseCanExecuteChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
                RaiseCanExecuteChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
                RaiseCanExecuteChanged();
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
                RaiseCanExecuteChanged();
            }
        }

        public bool CanProceed =>
            !string.IsNullOrWhiteSpace(FirstName) &&
            !string.IsNullOrWhiteSpace(LastName) &&
            !string.IsNullOrWhiteSpace(Email) &&
            BirthDate.HasValue;

        private async void Proceed()
        {
            try
            {
             //   await Task.Run(() =>
               // {
                 //   ValidateEmail(Email);
                   // ValidateBirthDate(BirthDate.Value);
                //});

                var person = new Person(FirstName, LastName, Email, BirthDate.Value);

                string result = $"Ім'я: {person.FirstName}\nПрізвище: {person.LastName}\nEmail: {person.Email}\n" +
                                $"Дата народження: {person.BirthDate.ToShortDateString()}\n" +
                                $"IsAdult: {person.IsAdult}\nSunSign: {person.SunSign}\n" +
                                $"ChineseSign: {person.ChineseSign}\nIsBirthday: {person.IsBirthday}";
                MessageBox.Show(result, "Результат");
            }
            catch (FutureBirthDateException ex)
            {
                MessageBox.Show(ex.Message, "Помилка дати");
            }
            catch (TooOldBirthDateException ex)
            {
                MessageBox.Show(ex.Message, "Помилка дати");
            }
            catch (InvalidEmailFormatException ex)
            {
                MessageBox.Show(ex.Message, "Помилка пошти");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Невідома помилка: " + ex.Message);
            }
        }

        private void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void RaiseCanExecuteChanged() =>
            _proceedCommand?.RaiseCanExecuteChanged();
    }
}
