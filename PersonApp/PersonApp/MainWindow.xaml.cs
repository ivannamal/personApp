using PersonApp;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PersonApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FirstNameBox.TextChanged += ValidateForm;
            LastNameBox.TextChanged += ValidateForm;
            EmailBox.TextChanged += ValidateForm;
            BirthDatePicker.SelectedDateChanged += ValidateForm;
        }

        private void ValidateForm(object sender, EventArgs e)
        {
            ProceedButton.IsEnabled =
                !string.IsNullOrWhiteSpace(FirstNameBox.Text) &&
                !string.IsNullOrWhiteSpace(LastNameBox.Text) &&
                !string.IsNullOrWhiteSpace(EmailBox.Text) &&
                BirthDatePicker.SelectedDate.HasValue;
        }

        private async void ProceedButton_Click(object sender, RoutedEventArgs e)
        {
            ProceedButton.IsEnabled = false;

            string firstName = FirstNameBox.Text;
            string lastName = LastNameBox.Text;
            string email = EmailBox.Text;
            DateTime birthDate = BirthDatePicker.SelectedDate.Value;

            try
            {
                Person person = await Task.Run(() =>
                {
                    ValidateEmail(email);
                    ValidateBirthDate(birthDate);
                    return new Person(firstName, lastName, email, birthDate);
                });

                string result = $"Ім'я: {person.FirstName}\nПрізвище: {person.LastName}\nEmail: {person.Email}\n" +
                                $"Дата народження: {person.BirthDate.ToShortDateString()}\n" +
                                $"IsAdult: {person.IsAdult}\nSunSign: {person.SunSign}\n" +
                                $"ChineseSign: {person.ChineseSign}\nIsBirthday: {person.IsBirthday}";
                MessageBox.Show(result, "Результат");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка");
            }
            finally
            {
                ProceedButton.IsEnabled = true;
            }
        }

        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^\S+@\S+\.\S+$"))
                throw new Exception("Невірний формат електронної пошти.");
        }

        private void ValidateBirthDate(DateTime date)
        {
            if (date > DateTime.Now)
                throw new Exception("Дата народження не може бути в майбутньому.");
        }
    }
}
