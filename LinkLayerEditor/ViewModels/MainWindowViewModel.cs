using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.Models;
using LinkLayerEditor.Views;
using XRMiniLink.Data;

namespace LinkLayerEditor.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<XLayerModel> _layers = new();
    

    [RelayCommand]
    private void DuplicateLayer()
    {
        if (!Layers.Any(x => x.IsSelected))
            return;
        var selectedLayers = Layers.Where(x => x.IsSelected).ToList();
        foreach (var layer in selectedLayers)
        {
            var newLayer = XLayerModel.FromDataLayer(layer.ToDataLayer());
            Layers.Add(newLayer);
        }
    }
    
    [RelayCommand]
    private void AddLayer()
    {
        var newLayer = new XLayerModel();
        Layers.Add(newLayer);
    }

    [RelayCommand]
    private void RemoveLayer()
    {
        if (!Layers.Any(x => x.IsSelected))
            return;
        var selectedLayers = Layers.Where(x => x.IsSelected).ToList();
        foreach (var layer in selectedLayers) 
            Layers.Remove(layer);
    }

    [RelayCommand]
    private async Task LoadLayers(Window parent)
    {
        var fpo = new FilePickerOpenOptions()
        {
            Title = "Open Layer File",
            AllowMultiple = false,
            FileTypeFilter = (List<FilePickerFileType>)
            [
                new FilePickerFileType("Layer Files")
                {
                    Patterns = (List<string>) ["*.xrml"]
                }
            ]
        };

        var result = await parent.StorageProvider.OpenFilePickerAsync(fpo);
        if (!result.Any())
            return;

        var filePath = result[0].TryGetLocalPath();
        if (filePath == null || !File.Exists(filePath))
            return;

        var layerData = await File.ReadAllTextAsync(filePath);
        var layers = JsonSerializer.Deserialize<XLayer[]>(layerData);
        if (layers == null)
            return;

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            Layers.Clear();
            foreach (var layer in layers) 
                Layers.Add(XLayerModel.FromDataLayer(layer));
        });
    }

    [RelayCommand]
    private async Task SaveLayers(Window parent)
    {
        var fpo = new FilePickerSaveOptions()
        {
            Title = "Save Layer File",
            DefaultExtension = "xrml",
            FileTypeChoices = (List<FilePickerFileType>)
            [
                new("Layer Files")
                {
                    Patterns = (List<string>) ["*.xrml"]
                }
            ]
        };

        var result = await parent.StorageProvider.SaveFilePickerAsync(fpo);
        if (result == null)
            return;

        var filePath = result.TryGetLocalPath();
        if (filePath == null)
            return;

        var layersToSave = Layers.Select(layer => layer.ToDataLayer()).ToArray();
        var jsonData = JsonSerializer.Serialize(layersToSave, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(filePath, jsonData);
    }

    [RelayCommand]
    private void OpenLiveLinkView()
    {
        if (!Layers.Any())
            return;
        
        var vm = new LiveLinkViewModel(Layers.Select(x => x.ToDataLayer()).ToList());
        var view = new LiveLinkView { DataContext = vm };
        view.Show();
    }
}