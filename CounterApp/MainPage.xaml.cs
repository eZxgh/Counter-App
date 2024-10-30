using CounterApp.Models;
namespace CounterApp
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Counter();
        }
    }

}
