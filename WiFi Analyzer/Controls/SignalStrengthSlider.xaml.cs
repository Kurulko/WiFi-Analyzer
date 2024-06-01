using Microcharts;
using WiFi_Analyzer.Models;

namespace WiFi_Analyzer.Controls;

public partial class SignalStrengthSlider : ContentView
{
    public static readonly BindableProperty NetworkStatesProperty = BindableProperty.Create(
        nameof(NetworkStates), typeof(NetworkStates), typeof(SignalStrengthSlider), propertyChanged: OnNetworkStatesChanged);

    public NetworkStates NetworkStates
    {
        get => (NetworkStates)GetValue(NetworkStatesProperty);
        set => SetValue(NetworkStatesProperty, value);
    }

    bool hasNetworkStates = false;
    public bool HasNetworkStates
    {
        get => hasNetworkStates;
        private set
        {
            hasNetworkStates = value;
            OnPropertyChanged(nameof(HasNetworkStates));
        }
    }

    public SignalStrengthSlider()
        => InitializeComponent();

    public int MaxSignalStrengthIndBm => -50;
    public int MinSignalStrengthIndBm => -100;


    static void OnNetworkStatesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var slider = (SignalStrengthSlider)bindable;
        slider.HasNetworkStates = newValue is not null;
    }
}
