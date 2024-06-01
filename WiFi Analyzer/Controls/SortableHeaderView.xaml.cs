using System.Windows.Input;
using WiFi_Analyzer.Enums;

namespace WiFi_Analyzer.Controls;

public partial class SortableHeaderView : ContentView
{
    public static readonly BindableProperty HeaderTextProperty = BindableProperty.Create(
        nameof(HeaderText), typeof(string), typeof(SortableHeaderView), string.Empty);

    public static readonly BindableProperty SortDirectionProperty = BindableProperty.Create(
        nameof(SortDirection), typeof(OrderBy?), typeof(SortableHeaderView), null);

    public static readonly BindableProperty SortCommandProperty = BindableProperty.Create(
        nameof(SortCommand), typeof(ICommand), typeof(SortableHeaderView), null);

    public static readonly BindableProperty SortCommandParameterProperty = BindableProperty.Create(
        nameof(SortCommandParameter), typeof(string), typeof(SortableHeaderView), string.Empty);

    public string HeaderText
    {
        get => (string)GetValue(HeaderTextProperty);
        set => SetValue(HeaderTextProperty, value);
    }

    public OrderBy? SortDirection
    {
        get => (OrderBy?)GetValue(SortDirectionProperty);
        set => SetValue(SortDirectionProperty, value);
    }

    public ICommand SortCommand
    {
        get => (ICommand)GetValue(SortCommandProperty);
        set => SetValue(SortCommandProperty, value);
    }

    public string SortCommandParameter
    {
        get => (string)GetValue(SortCommandParameterProperty);
        set => SetValue(SortCommandParameterProperty, value);
    }

    public SortableHeaderView()
        => InitializeComponent();
}