using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BirthdayApp
{
    public partial class BirthdayViewModel : INotifyPropertyChanged
    {
        private DateTime? birthday;
        private string ageText;
        private string westernZodiac;
        private string chineseZodiac;
        public event PropertyChangedEventHandler PropertyChanged;

        public DateTime? Birthday
        {
            get => birthday;
            set
            {
                birthday = value;
                OnPropertyChanged(nameof(Birthday));
            }
        }

        public string AgeText
        {
            get => ageText;
            set
            {
                ageText = value;
                OnPropertyChanged(nameof(AgeText));
            }
        }

        public string WesternZodiac
        {
            get => westernZodiac;
            set
            {
                westernZodiac = value;
                OnPropertyChanged(nameof(WesternZodiac));
            }
        }

        public string ChineseZodiac
        {
            get => chineseZodiac;
            set
            {
                chineseZodiac = value;
                OnPropertyChanged(nameof(ChineseZodiac));
            }
        }

        public ICommand CalculateCommand => new RelayCommand(CalculateAgeAndZodiac);

        private void CalculateAgeAndZodiac()
        {
            if (!Birthday.HasValue)
            {
                MessageBox.Show("Please select your birthday.");
                return;
            }

            DateTime selectedDate = Birthday.Value;
            int age = DateTime.Today.Year - selectedDate.Year;
            if (DateTime.Today < selectedDate.AddYears(age)) age--;

            if (age > 135 || selectedDate > DateTime.Today)
            {
                MessageBox.Show("Invalid date of birth.");
                return;
            }

            AgeText = $"Your age is {age}";

            WesternZodiac = $"Western Zodiac: {GetWesternZodiac(selectedDate)}";
            ChineseZodiac = $"Chinese Zodiac: {GetChineseZodiac(selectedDate)}";

            if (DateTime.Today.Day == selectedDate.Day && DateTime.Today.Month == selectedDate.Month)
            {
                MessageBox.Show("Happy Birthday!");
            }
        }

        private string GetWesternZodiac(DateTime date)
        {
            int day = date.Day, month = date.Month;
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

        private string GetChineseZodiac(DateTime date)
        {
            string[] animals = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake",
                                 "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            int index = (date.Year - 1900) % 12;
            return animals[index];
        }

        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
