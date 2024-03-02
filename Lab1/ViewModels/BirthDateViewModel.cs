using Lab1Mindiuk.Models;
using Lab1Mindiuk.Tools;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Lab1Mindiuk.ViewModels
{
    internal class BirthDateViewModel : INotifyPropertyChanged
    {
        private UserCandidate _user = new UserCandidate();
        private string _age;
        private string _westernZodiacSign;
        private string _chineseZodiacSign;
        private RelayCommand<object> _showBirthDateInfoCommand;

        public string Age
        {
            get 
            { 
                return _age;
            }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    _ = int.TryParse(value, out int age);
                    _age = (age >= 0 && age < 135) ? $"{age} y.o." : string.Empty;
                }
                else
                {
                    _age = string.Empty;
                }
                OnPropertyChanged();
            }
        }

        public string WesternZodiacSign
        {
            get 
            { 
                return _westernZodiacSign;
            }
            set
            {
                _westernZodiacSign = value;
                OnPropertyChanged();
                
            }
        }

        public string ChineseZodiacSign
        {
            get 
            {
                return _chineseZodiacSign; 
            }
            set
            {
                _chineseZodiacSign = value;
                OnPropertyChanged();
                
            }
        }

        public DateTime BirthDate
        {
            get 
            {
                return _user.BirthDate;
            }
            set
            {
                _user.BirthDate = value;
            }
        }

        public RelayCommand<object> ShowBirthDateInfoCommand => _showBirthDateInfoCommand ??= new RelayCommand<object>(_ => ShowBirthDateInfo(), CanExecute);

        private bool CanExecute(object parameter) => BirthDate != DateTime.MinValue;


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ShowBirthDateInfo()
        {
            int age = DetermineAge();
               
            if (age < 0)
            {
                ClearInformation();
                MessageBox.Show("You're not even born yet. Choose proper date!");
                return;
            }
            else if (age > 135)
            {
                ClearInformation();
                MessageBox.Show("Are you for real that old? Don't play jokes around. Choose proper date!");
                return;
            }
            else
            {
                Age = age.ToString();
                WesternZodiacSign = DetermineWesternZodiacSign(BirthDate);
                ChineseZodiacSign = DetermineChineseZodiacSign(BirthDate);
                if (BirthDate.Month == DateTime.Today.Month && BirthDate.Day == DateTime.Today.Day)
                {
                    string yearsOld = (age == 1) ? "year" : "years";
                    string birthdayGreeting = (age == 0) ? "You were born today! Welcome to the world! 🎉👶"
                                                        : $"Happy Birthday! Another year older, wiser, and more fabulous! *.·:·.✧ ✦ ✧.·:·.*" +
                                                          $" Today you are {age} {yearsOld} old. Enjoy your special day! 🎉🎂🎈";
                    MessageBox.Show(birthdayGreeting);
                }

            }
        }


        private int DetermineAge()
        {
            DateTime today = DateTime.Today;

            int age = today.Year - BirthDate.Year;

            if (today.Month < BirthDate.Month || (today.Month == BirthDate.Month && today.Day < BirthDate.Day))
            {
                age--;
            }

            return age;
        }

        private void ClearInformation()
        {
            Age = string.Empty;
            WesternZodiacSign = string.Empty;
            ChineseZodiacSign = string.Empty;
        }

        private string DetermineWesternZodiacSign(DateTime birthDate)
        {
            int day = birthDate.Day;
            int month = birthDate.Month;

            string[] zodiacSigns = 
            {
                "Capricorn", "Aquarius", "Pisces", "Aries",
                "Taurus", "Gemini", "Cancer", "Leo",
                "Virgo", "Libra", "Scorpio", "Sagittarius"
            };

            
            int[] startZodiacSignDate = 
            {
                20, 19, 21, 20,
                21, 21, 23, 23,
                23, 23, 22, 22
            };

           
            int index = (month - 1 + (day >= startZodiacSignDate[month - 1] ? 1 : 0)) % 12;

            return zodiacSigns[index];
        }



        private string DetermineChineseZodiacSign(DateTime birthDate)
        {
            ChineseZodiacSigns chineseZodiacSign = (ChineseZodiacSigns)(birthDate.Year % 12);
            return chineseZodiacSign.ToString();
        }

        enum ChineseZodiacSigns
        {
            Monkey, Rooster, Dog, Pig,
            Rat, Ox, Tiger, Rabbit,
            Dragon, Snake, Horse, Sheep
        }
    }
}
