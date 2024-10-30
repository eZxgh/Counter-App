using CounterApp.Models;
using System.Text.Json;

namespace CounterApp.Views;

public partial class CounterPage : ContentPage
{
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "counters.txt");
    public CounterPage()
    {
        InitializeComponent();
        LoadCounters();
    }

    private void SaveCounters()
    {
        if (BindingContext is Counter counter)
        {
            try
            {
                string json = JsonSerializer.Serialize(counter.Counters);

                File.WriteAllText(_filePath, json);
            }
            catch (Exception e)
            {
                DisplayAlert("Error", $"Failed to save counters: {e.Message}", "OK");
            }
        }
    }

    private void LoadCounters()
    {
        if (File.Exists(_filePath))
        {
            try
            {
                string json = File.ReadAllText(_filePath);

                var counters = JsonSerializer.Deserialize<List<CounterModel>>(json);

                if (BindingContext is Counter counter && counters != null)
                {
                    foreach (var loadedCounter in counters)
                    {
                        counter.Counters.Add(loadedCounter);
                    }
                }
            }
            catch (Exception e)
            {
                DisplayAlert("Error", $"Failed to load counters: {e.Message}", "OK");
            }
        }
    }

    private void OnCounterAdd_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Counter counter)
        {
            string newItemText = CounterTitle.Text;
            bool isValidNumber = int.TryParse(CounterValue.Text, out int initialValue);

            if (!string.IsNullOrEmpty(newItemText))
            {
                if (newItemText.Length > 25)
                {
                    DisplayAlert("Title cannot be longer than 25 characters", "Please enter a valid title", "OK");
                    CounterTitle.Text = string.Empty;
                }
                else if (!isValidNumber || initialValue > int.MaxValue || initialValue < int.MinValue)
                {
                    DisplayAlert("Invalid Initial Value", $"Please enter a number that is higher that {int.MinValue} and less or equal to {int.MaxValue}.", "OK");
                    CounterValue.Text = string.Empty;
                }
                else
                {
                    var newCounter = new CounterModel(newItemText, initialValue);
                    counter.Counters.Add(newCounter);
                    CounterTitle.Text = string.Empty;
                    CounterValue.Text = string.Empty;
                    SaveCounters();
                }
            }
            else
            {
                DisplayAlert("Title cannot be empty.", "Please enter a valid title", "OK");
            }
        }
    }

    private void OnIncrement_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CounterModel counterModel)
        {
            counterModel.Value += 1;
            SaveCounters();
        }
    }

    private void OnDecrement_Clicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is CounterModel counterModel)
        {
            counterModel.Value -= 1;
            SaveCounters();
        }
    }
}
