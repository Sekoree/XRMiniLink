using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace LinkLayerEditor.Dialogs;

public partial class AskStringDialog : Window
{
    public AskStringDialog()
    {
        InitializeComponent();
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        this.Close(null);
    }

    private void OkButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var val = this.InputTextBox.Text;
        this.Close(string.IsNullOrWhiteSpace(val) ? null : val);
    }
}