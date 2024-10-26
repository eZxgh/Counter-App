using CounterApp.Models;
using System.Diagnostics;
using System.Diagnostics.Metrics;

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
            var elements = counter.Counters.Select(x => $"{x.Title}|{x.Value}").ToList();
            File.WriteAllLines(_filePath, elements);
        }
    }

    private void LoadCounters()
    {
        if (BindingContext is Counter counter)
        {
            if(File.Exists(_filePath))
            {
                var elements = File.ReadAllLines(_filePath);
                foreach(var line in elements)
                {
                    var values = line.Split('|');
                    if(values.Length == 2 && int.TryParse(values[1], out int value))
                    {
                        var newCounter = new CounterModel(values[0]) { Value = value };
                        counter.Counters.Add(newCounter);
                    }
                }
            }
        }
    }

    private void OnCounterAdd_Clicked(object sender, EventArgs e)
    {
        if (BindingContext is Counter counter)
        {
            string newItemText = CounterTitle.Text;

            if (!string.IsNullOrEmpty(newItemText))
            {
                if(newItemText.Length > 25)
                {
                    DisplayAlert("Title cannot be longer than 25 characters", "Please enter a valid title", "OK");
                    CounterTitle.Text = string.Empty;
                }
                else
                {
                    var newCounter = new CounterModel(newItemText) { Value = 0 };
                    counter.Counters.Add(newCounter);
                    CounterTitle.Text = string.Empty;
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
