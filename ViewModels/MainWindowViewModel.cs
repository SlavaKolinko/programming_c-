using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Kolinko_lab_01.Commands;

namespace Kolinko_lab_01.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private const int MaxAge = 122;

        private DateTime? _birthDate;
        private string _age;
        private string _westernZodiac;
        private string _chineseZodiac;

#pragma warning disable CS8612
        public event PropertyChangedEventHandler PropertyChanged;
#pragma warning restore CS8612 

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged();
            }
        }

        public string Age
        {
            get => _age;
            set
            {
                _age = value;
                OnPropertyChanged();
            }
        }

        public string WesternZodiac
        {
            get => _westernZodiac;
            set
            {
                _westernZodiac = value;
                OnPropertyChanged();
            }
        }

        public string ChineseZodiac
        {
            get => _chineseZodiac;
            set
            {
                _chineseZodiac = value;
                OnPropertyChanged();
            }
        }

        public ICommand CalculateCommand { get; }

        public MainViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
        }

        private void Calculate()
        {
            if (BirthDate == null)
            {
                MessageBox.Show("Please input birthday date");
                return;
            }

            var today = DateTime.Today;
            var birth = BirthDate.Value;
            var age = today.Year - birth.Year;

            if (birth > today.AddYears(-age)) age--;

            if (birth > today)
            {
                MessageBox.Show("Hmm..It seams you weren't born yet");
                return;
            }

            if (age > MaxAge)
            {
                MessageBox.Show("I doubt it)");
                return;
            }

            Age = age.ToString();
            WesternZodiac = GetWesternZodiac(birth);
            ChineseZodiac = GetChineseZodiac(birth);

            if (birth.Month == today.Month && birth.Day == today.Day)
            {
                MessageBox.Show("Happy birthday! 🎉");
            }
        }

        private string GetWesternZodiac(DateTime date)
        {
            var day = date.Day;
            var month = date.Month;

            return (month, day) switch
            {
                (1, <= 19) => "Capricorn",
                (1, _) => "Aquarius",
                (2, <= 18) => "Aquarius",
                (2, _) => "Pisces",
                (3, <= 20) => "Pisces",
                (3, _) => "Aries",
                (4, <= 19) => "Aries",
                (4, _) => "Taurus",
                (5, <= 20) => "Taurus",
                (5, _) => "Gemini",
                (6, <= 20) => "Gemini",
                (6, _) => "Cancer",
                (7, <= 22) => "Cancer",
                (7, _) => "Leo",
                (8, <= 22) => "Leo",
                (8, _) => "Virgo",
                (9, <= 22) => "Virgo",
                (9, _) => "Libra",
                (10, <= 22) => "Libra",
                (10, _) => "Scorpio",
                (11, <= 21) => "Scorpio",
                (11, _) => "Sagittarius",
                (12, <= 21) => "Sagittarius",
                (12, _) => "Capricorn",
                _ => "Unknown"
            };
        }
        private string GetChineseZodiac(DateTime date)
        {
            var animals = new string[]
            {
        "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig"
            };

            var chineseNewYearDates = new Dictionary<int, DateTime>
    {

    };

            DateTime chineseNewYear = chineseNewYearDates.ContainsKey(date.Year) ? chineseNewYearDates[date.Year] : new DateTime(date.Year, 2, 19);

            int zodiacYear = (date < chineseNewYear) ? date.Year - 1 : date.Year;
            int zodiacIndex = (zodiacYear - 1900) % 12;

            return animals[zodiacIndex];
        }


        protected void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
