using PersonApp.Data;
//using PersonApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PersonApp
{
    public partial class UsersWindow : Window
    {
        private List<Person> persons;

        public UsersWindow()
        {
            InitializeComponent();
            LoadOrInitializeData();
            dgUsers.ItemsSource = persons;
        }

        private void LoadOrInitializeData()
        {
            persons = PersonDataManager.Load();
            // Якщо даних немає – створюємо 50 демонстраційних записів
            if (persons.Count == 0)
            {
                persons = new List<Person>();
                Random rnd = new Random();
                for (int i = 1; i <= 50; i++)
                {
                    string firstName = $"Ім'я{i}";
                    string lastName = $"Прізвище{i}";
                    string email = $"user{i}@example.com";
                    // Генеруємо дату народження від 1960 до 2010 року
                    DateTime dob = new DateTime(rnd.Next(1960, 2010), rnd.Next(1, 13), rnd.Next(1, 28));
                    try
                    {
                        persons.Add(new Person(firstName, lastName, email, dob));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Помилка створення користувача: {ex.Message}");
                    }
                }
                PersonDataManager.Save(persons);
            }
        }

        // Обробник кнопки "Додати"
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            EditUserWindow addWindow = new EditUserWindow();
            addWindow.Owner = this;
            if (addWindow.ShowDialog() == true)
            {
                persons.Add(addWindow.EditedPerson);
                dgUsers.ItemsSource = null;
                dgUsers.ItemsSource = persons;
                PersonDataManager.Save(persons);
            }
        }

        // Обробник кнопки "Редагувати"
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is Person selectedPerson)
            {
                EditUserWindow editWindow = new EditUserWindow(selectedPerson);
                editWindow.Owner = this;
                if (editWindow.ShowDialog() == true)
                {
                    int index = persons.IndexOf(selectedPerson);
                    if (index >= 0)
                    {
                        persons[index] = editWindow.EditedPerson;
                        dgUsers.ItemsSource = null;
                        dgUsers.ItemsSource = persons;
                        PersonDataManager.Save(persons);
                    }
                }
            }
            else
            {
                MessageBox.Show("Будь ласка, оберіть користувача для редагування.", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        // Обробник кнопки "Видалити" залишається без змін
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is Person selectedPerson)
            {
                if (MessageBox.Show("Видалити користувача?", "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    persons.Remove(selectedPerson);
                    dgUsers.ItemsSource = null;
                    dgUsers.ItemsSource = persons;
                    PersonDataManager.Save(persons);
                }
            }
        }

        // Метод сортування (приклад)
        private void SortByFirstName()
        {
            var sorted = persons.OrderBy(p => p.FirstName).ToList();
            dgUsers.ItemsSource = sorted;
        }
    }
}
