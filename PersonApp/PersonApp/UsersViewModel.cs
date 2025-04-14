using PersonApp.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PersonApp
{
    public class UsersViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> _persons;
        public ObservableCollection<Person> Persons
        {
            get => _persons;
            set
            {
                _persons = value;
                OnPropertyChanged(nameof(Persons));
            }
        }

        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get => _selectedPerson;
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
                EditCommand.RaiseCanExecuteChanged();
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand AddCommand { get; }
        public RelayCommand EditCommand { get; }
        public RelayCommand DeleteCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public UsersViewModel()
        {
            LoadOrInitializeData();
            AddCommand = new RelayCommand(AddPerson);
            EditCommand = new RelayCommand(EditPerson, () => SelectedPerson != null);
            DeleteCommand = new RelayCommand(DeletePerson, () => SelectedPerson != null);
        }

        private void LoadOrInitializeData()
        {
            
            List<Person> personsList = PersonDataManager.Load();

            if (personsList.Count == 0)
            {
                personsList = new List<Person>();
                Random rnd = new Random();
                for (int i = 1; i <= 50; i++)
                {
                    string firstName = $"Ім'я{i}";
                    string lastName = $"Прізвище{i}";
                    string email = $"user{i}@example.com";
                   
                    DateTime dob = new DateTime(rnd.Next(1960, 2010), rnd.Next(1, 13), rnd.Next(1, 28));
                    try
                    {
                        personsList.Add(new Person(firstName, lastName, email, dob));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка створення користувача: {ex.Message}");
                    }
                }
                PersonDataManager.Save(personsList);
            }
            Persons = new ObservableCollection<Person>(personsList);
        }

        private void AddPerson()
        {
            
            var editVm = new EditUserViewModel();
            var editWindow = new EditUserWindow
            {
                DataContext = editVm,
                Owner = Application.Current.MainWindow
            };

            if (editWindow.ShowDialog() == true)
            {
                if (editVm.EditedPerson != null)
                {
                    Persons.Add(editVm.EditedPerson);
                    PersonDataManager.Save(Persons.ToList());
                }
            }
        }

        private void EditPerson()
        {
            if (SelectedPerson != null)
            {
                var editVm = new EditUserViewModel
                {
                    FirstName = SelectedPerson.FirstName,
                    LastName = SelectedPerson.LastName,
                    Email = SelectedPerson.Email,
                    BirthDate = SelectedPerson.BirthDate
                };

                var editWindow = new EditUserWindow
                {
                    DataContext = editVm,
                    Owner = Application.Current.MainWindow
                };

                if (editWindow.ShowDialog() == true)
                {
                    if (editVm.EditedPerson != null)
                    {
                        int index = Persons.IndexOf(SelectedPerson);
                        if (index >= 0)
                        {
                            Persons[index] = editVm.EditedPerson;
                            SelectedPerson = Persons[index];
                            PersonDataManager.Save(Persons.ToList());
                        }
                    }
                }
            }
        }

        private void DeletePerson()
        {
            if (SelectedPerson != null)
            {
                if (MessageBox.Show("Видалити користувача?", "Підтвердження",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Persons.Remove(SelectedPerson);
                    PersonDataManager.Save(Persons.ToList());
                }
            }
        }

        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
