using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

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
