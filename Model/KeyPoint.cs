using System;
using BookingApp.ViewModel;

namespace BookingApp.Model
{
    public class KeyPoint : ViewModelBase
    {
        public string Name { get; set; }

        private bool _isMarked;
        public bool IsMarked
        {
            get => _isMarked;
            set
            {
                if (_isMarked != value)
                {
                    _isMarked = value;
                    OnPropertyChanged(nameof(IsMarked));
                }
            }
        }

        public KeyPoint() { }

        public KeyPoint(string name, bool isMarked)
        {
            Name = name;
            IsMarked = isMarked;
        }

        public string ToCSV()
        {
            return $"{Name}:{IsMarked}";
        }

        public static KeyPoint FromCSV(string value)
        {
            string[] values = value.Split(':');
            return new KeyPoint(values[0], Convert.ToBoolean(values[1]));
        }
    }
}
