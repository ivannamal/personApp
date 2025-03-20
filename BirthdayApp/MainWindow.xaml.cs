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
                    MessageBox.Show("tf??? not a valid date!!!");
                }
                else
                {
                    MessageBox.Show($"Your birthday is {birthday.Value.ToShortDateString()}");
                    MessageBox.Show($"Your age is {age}. btw ur {GetWesternZodiac(birthday.Value)} and {GetChineseZodiac(birthday.Value)}");
                    if(DateTime.Today.Day==birthday.Value.Day && DateTime.Today.Month == birthday.Value.Month)
                    {
                        MessageBox.Show("Damn its your birthday!!! yuppiee");
                    }
                }
            }
            else
            {
                MessageBox.Show("error!!!");
            }
        }
        string GetWesternZodiac(DateTime date)
        {
            int day = date.Day;
            int month = date.Month;

            return (month == 1 && day <= 19) ? "Козоріг" :
                   (month == 1) ? "Водолій" :
                   (month == 2 && day <= 18) ? "Водолій" :
                   (month == 2) ? "Риби" :
                   (month == 3 && day <= 20) ? "Риби" :
                   (month == 3) ? "Овен" :
                   (month == 4 && day <= 19) ? "Овен" :
                   (month == 4) ? "Телець" :
                   (month == 5 && day <= 20) ? "Телець" :
                   (month == 5) ? "Близнюки" :
                   (month == 6 && day <= 20) ? "Близнюки" :
                   (month == 6) ? "Рак" :
                   (month == 7 && day <= 22) ? "Рак" :
                   (month == 7) ? "Лев" :
                   (month == 8 && day <= 22) ? "Лев" :
                   (month == 8) ? "Діва" :
                   (month == 9 && day <= 22) ? "Діва" :
                   (month == 9) ? "Терези" :
                   (month == 10 && day <= 22) ? "Терези" :
                   (month == 10) ? "Скорпіон" :
                   (month == 11 && day <= 21) ? "Скорпіон" :
                   (month == 11) ? "Стрілець" :
                   (month == 12 && day <= 21) ? "Стрілець" : "Козоріг";
        }
        string GetChineseZodiac(DateTime date)
        {
            string[] animals = { "Мавпа", "Півень", "Собака", "Свиня", "Щур", "Бик",
                         "Тигр", "Кролик", "Дракон", "Змія", "Кінь", "Коза" };
            int index = date.Year % 12;
            return animals[index];
        }

    }
}