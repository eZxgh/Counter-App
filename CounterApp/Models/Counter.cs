using System.Collections.ObjectModel;

namespace CounterApp.Models
{
    public class Counter 
    {
        public ObservableCollection<CounterModel> Counters { get; set; }

        public Counter()
        {
            Counters = new ObservableCollection<CounterModel>();
        }
    }
}
