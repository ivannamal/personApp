using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BirthdayApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime? birthday = BirthdayPicker.SelectedDate;
             
            if(birthday.HasValue) {
                int age = DateTime.Today.Year - birthday.Value.Year;
                if (DateTime.Today < birthday.Value.AddYears(age))
                {
                    age--;
                }
                
                if (((DateTime.Today.Year - birthday.Value.Year) > 135) || birthday > DateTime.Today)
                {
                    MessageBox.Show("This is not a valid date.");
                }
                else
                {
                    MessageBox.Show($"Your birthday is {birthday.Value.ToShortDateString()}");
                    MessageBox.Show($"Your age is {age}. Your western zodiac sign is {GetWesternZodiac(birthday.Value)} and your chinese zodiac sign is {GetChineseZodiac(birthday.Value)}");
                    if(DateTime.Today.Day==birthday.Value.Day && DateTime.Today.Month == birthday.Value.Month)
                    {
                        MessageBox.Show("Today is your birthday! Congrats!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Error.");
            }
        }
        string GetWesternZodiac(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            return (month == 1 && day <= 19) ? "Capricorn" :
                   (month == 1) ? "Aquarius" :
                   (month == 2 && day <= 18) ? "Aquarius" :
                   (month == 2) ? "Pisces" :
                   (month == 3 && day <= 20) ? "Pisces" :
                   (month == 3) ? "Aries" :
                   (month == 4 && day <= 19) ? "Aries" :
                   (month == 4) ? "Taurus" :
                   (month == 5 && day <= 20) ? "Taurus" :
                   (month == 5) ? "Gemini" :
                   (month == 6 && day <= 20) ? "Gemini" :
                   (month == 6) ? "Cancer" :
                   (month == 7 && day <= 22) ? "Cancer" :
                   (month == 7) ? "Leo" :
                   (month == 8 && day <= 22) ? "Leo" :
                   (month == 8) ? "Virgo" :
                   (month == 9 && day <= 22) ? "Virgo" :
                   (month == 9) ? "Libra" :
                   (month == 10 && day <= 22) ? "Libra" :
                   (month == 10) ? "Scorpio" :
                   (month == 11 && day <= 21) ? "Scorpio" :
                   (month == 11) ? "Sagittarius" :
                   (month == 12 && day <= 21) ? "Sagittarius" : "Capricorn";
        }
        string GetChineseZodiac(DateTime date)
        {
            string[] animals = { "Monkey", "Rooster", "Dog", "Pig", "Rat", "Ox",
                         "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat" };
            int index = date.Year % 12;
            return animals[index];
        }

    }
}