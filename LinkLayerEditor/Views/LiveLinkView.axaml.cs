using System;
using Avalonia.Controls;

namespace LinkLayerEditor.Views;

public partial class LiveLinkView : Window
{
    public LiveLinkView()
    {
        InitializeComponent();
    }

    protected override void OnClosing(WindowClosingEventArgs e)
    {
        if (DataContext is IDisposable disposable) 
            disposable.Dispose();
        base.OnClosing(e);
    }
}