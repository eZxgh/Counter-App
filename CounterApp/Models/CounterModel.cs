using System.ComponentModel;

namespace CounterApp.Models
{
    public class CounterModel : INotifyPropertyChanged
    {
        private int _value;
        public string Title { get; set; }

        public int Value
        {
            get => _value;
            set
            {
                if (_value != value)
                {
                    _value = value;
                    OnPropertyChanged(nameof(Value));
                }
            }
        }

        public CounterModel(string title)
        {
            Title = title;
            Value = 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}