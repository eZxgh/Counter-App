using System.ComponentModel;

namespace CounterApp.Models
{
    public class CounterModel : INotifyPropertyChanged
    {
        public string Title { get; set; }

        private int _value;

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
        public CounterModel() { }
        public CounterModel(string title,int initialValue)
        {
            Title = title;
            Value = initialValue;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}