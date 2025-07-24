using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XLayerModel : ViewModelBase
{
    [ObservableProperty]
    private bool _isSelected;
    
    [ObservableProperty]
    private string _name = "Unnamed Layer";
    
    [ObservableProperty]
    private bool _isDefault;
    
    [ObservableProperty]
    private ObservableCollection<XBandModel> _bands = new();
    
    //[ObservableProperty]
    //private XBandModel? _selectedBand;
    
    [ObservableProperty]
    private bool _hasFader;
    
    [ObservableProperty]
    private XFaderModel? _fader;
    
    [ObservableProperty]
    private bool _hasButtonUp;
    
    [ObservableProperty]
    private XButtonModel? _buttonUp;
    
    [ObservableProperty]
    private bool _hasButtonUpLED;
    
    [ObservableProperty]
    private XButtonLEDModel? _buttonUpLED;
    
    [ObservableProperty]
    private bool _hasButtonDown;
    
    [ObservableProperty]
    private XButtonModel? _buttonDown;
    
    [ObservableProperty]
    private bool _hasButtonDownLED;
    
    [ObservableProperty]
    private XButtonLEDModel? _buttonDownLED;
    
    [ObservableProperty]
    private ObservableCollection<XOSCCommandModel> _initOSCCommands = new();
    
    [ObservableProperty]
    private XOSCCommandModel? _selectedInitOSCCommand;
    
    [ObservableProperty]
    private ObservableCollection<XInternalCommandModel> _initInternalCommands = new();
    
    [ObservableProperty]
    private XInternalCommandModel? _selectedInitInternalCommand;
    
    [ObservableProperty]
    private ObservableCollection<XMIDICommandModel> _initMIDICommands = new();
    
    [ObservableProperty]
    private XMIDICommandModel? _selectedInitMIDICommand;
    
    [ObservableProperty]
    private ObservableCollection<XOSCCommandModel> _unInitOscCommands = new();
    
    [ObservableProperty]
    private XOSCCommandModel? _selectedUnInitOscCommand;
    
    [ObservableProperty]
    private ObservableCollection<XInternalCommandModel> _unInitInternalCommands = new();
    
    [ObservableProperty]
    private XInternalCommandModel? _selectedUnInitInternalCommand;
    
    [ObservableProperty]
    private ObservableCollection<XMIDICommandModel> _unInitMIDICommands = new();
    
    [ObservableProperty]
    private XMIDICommandModel? _selectedUnInitMIDICommand;
    
    //[RelayCommand]
    //private void AddBand()
    //{
    //    //Up to 8 bands are allowed
    //    if (Bands.Count >= 8)
    //        return;
    //    Bands.Add(new XBandModel());
    //}

    //[RelayCommand]
    //private void RemoveBand()
    //{
    //    if (SelectedBand == null)
    //        return;
    //    //Remove the selected band
    //    Bands.Remove(SelectedBand);
    //    SelectedBand = null;
    //}
    
    [RelayCommand]
    private void AddInitOSCCommand()
    {
        InitOSCCommands.Add(new XOSCCommandModel());
    }
    
    [RelayCommand]
    private void RemoveInitOSCCommand()
    {
        if (SelectedInitOSCCommand == null)
            return;

        //Remove the selected init OSC command
        InitOSCCommands.Remove(SelectedInitOSCCommand);
        SelectedInitOSCCommand = null;
    }
    
    [RelayCommand]
    private void AddInitInternalCommand()
    {
        InitInternalCommands.Add(new XInternalCommandModel());
    }
    
    [RelayCommand]
    private void RemoveInitInternalCommand()
    {
        if (SelectedInitInternalCommand == null)
            return;

        //Remove the selected init internal command
        InitInternalCommands.Remove(SelectedInitInternalCommand);
        SelectedInitInternalCommand = null;
    }
    
    [RelayCommand]
    private void AddInitMIDICommand()
    {
        InitMIDICommands.Add(new XMIDICommandModel());
    }
    
    [RelayCommand]
    private void RemoveInitMIDICommand()
    {
        if (SelectedInitMIDICommand == null)
            return;

        //Remove the selected init MIDI command
        InitMIDICommands.Remove(SelectedInitMIDICommand);
        SelectedInitMIDICommand = null;
    }
    
    
    [RelayCommand]
    private void AddUnInitOSCCommand()
    {
        UnInitOscCommands.Add(new XOSCCommandModel());
    }
    
    [RelayCommand]
    private void RemoveUnInitOSCCommand()
    {
        if (SelectedUnInitOscCommand == null)
            return;

        //Remove the selected uninit OSC command
        UnInitOscCommands.Remove(SelectedUnInitOscCommand);
        SelectedUnInitOscCommand = null;
    }
    
    [RelayCommand]
    private void AddUnInitInternalCommand()
    {
        UnInitInternalCommands.Add(new XInternalCommandModel());
    }
    
    [RelayCommand]
    private void RemoveUnInitInternalCommand()
    {
        if (SelectedUnInitInternalCommand == null)
            return;

        //Remove the selected uninit internal command
        UnInitInternalCommands.Remove(SelectedUnInitInternalCommand);
        SelectedUnInitInternalCommand = null;
    }
    
    [RelayCommand]
    private void AddUnInitMIDICommand()
    {
        UnInitMIDICommands.Add(new XMIDICommandModel());
    }
    
    [RelayCommand] 
    private void RemoveUnInitMIDICommand()
    {
        if (SelectedUnInitMIDICommand == null)
            return;

        //Remove the selected uninit MIDI command
        UnInitMIDICommands.Remove(SelectedUnInitMIDICommand);
        SelectedUnInitMIDICommand = null;
    }
    

    partial void OnHasFaderChanged(bool value)
    {
        Fader = value switch
        {
            true when Fader == null => new XFaderModel()
            {
                MidiNote = XTouchMiniNotes.Fader
            },
            false => null,
            _ => Fader
        };
    }
    
    partial void OnHasButtonUpChanged(bool value)
    {
        ButtonUp = value switch
        {
            true when ButtonUp == null => new XButtonModel()
            {
                MidiNote = XTouchMiniNotes.ButtonLayerUp
            },
            false => null,
            _ => ButtonUp
        };
    }
    
    partial void OnHasButtonUpLEDChanged(bool value)
    {
        ButtonUpLED = value switch
        {
            true when ButtonUpLED == null => new XButtonLEDModel()
            {
                MidiNote = XTouchMiniNotes.ButtonLayerUp
            },
            false => null,
            _ => ButtonUpLED
        };
    }
    
    partial void OnHasButtonDownChanged(bool value)
    {
        ButtonDown = value switch
        {
            true when ButtonDown == null => new XButtonModel()
            {
                MidiNote = XTouchMiniNotes.ButtonLayerDown
            },
            false => null,
            _ => ButtonDown
        };
    }
    
    partial void OnHasButtonDownLEDChanged(bool value)
    {
        ButtonDownLED = value switch
        {
            true when ButtonDownLED == null => new XButtonLEDModel()
            {
                MidiNote = XTouchMiniNotes.ButtonLayerDown
            },
            false => null,
            _ => ButtonDownLED
        };
    }

    public XLayerModel()
    {
        //add 8 empty bands by default
        for (var i = 0; i < 8; i++) 
            Bands.Add(new XBandModel(i + 1));
    }
    
    public XLayer ToDataLayer()
    {
        return new XLayer
        {
            Name = Name.Trim(),
            IsDefault = IsDefault,
            Bands = [..Bands.Select(b => b.ToDataLayer())],
            Fader = HasFader ? Fader?.ToDataLayer() : null,
            ButtonUp = HasButtonUp ? ButtonUp?.ToDataLayer() : null,
            ButtonUpLED = HasButtonUpLED ? ButtonUpLED?.ToDataLayer() : null,
            ButtonDown = HasButtonDown ? ButtonDown?.ToDataLayer() : null,
            ButtonDownLED = HasButtonDownLED ? ButtonDownLED?.ToDataLayer() : null,
            InitOSCCommmands = [..InitOSCCommands.Select(c => c.ToDataLayer())],
            InitInternalCommands = [..InitInternalCommands.Select(c => c.ToDataLayer())],
            InitMIDICommands = [..InitMIDICommands.Select(c => c.ToDataLayer())],
            UnInitOSCCommands = [..UnInitOscCommands.Select(c => c.ToDataLayer())],
            UnInitInternalCommands = [..UnInitInternalCommands.Select(c => c.ToDataLayer())],
            UnInitMIDICommands = [..UnInitMIDICommands.Select(c => c.ToDataLayer())]
        };
    }
    
    public static XLayerModel FromDataLayer(XLayer layer)
    {
        var bands = new ObservableCollection<XBandModel>();
        for (var i = 0; i < layer.Bands.Count; i++)
        {
            var band = XBandModel.FromDataLayer(layer.Bands[i], i + 1);
            bands.Add(band);
        }
        
        return new XLayerModel
        {
            Name = layer.Name.Trim(),
            IsDefault = layer.IsDefault,
            Bands = bands,
            HasFader = layer.Fader != null,
            Fader = layer.Fader != null ? XFaderModel.FromDataLayer(layer.Fader) : null,
            HasButtonUp = layer.ButtonUp != null,
            ButtonUp = layer.ButtonUp != null ? XButtonModel.FromDataLayer(layer.ButtonUp) : null,
            HasButtonUpLED = layer.ButtonUpLED != null,
            ButtonUpLED = layer.ButtonUpLED != null ? XButtonLEDModel.FromDataLayer(layer.ButtonUpLED) : null,
            HasButtonDown = layer.ButtonDown != null,
            ButtonDown = layer.ButtonDown != null ? XButtonModel.FromDataLayer(layer.ButtonDown) : null,
            HasButtonDownLED = layer.ButtonDownLED != null,
            ButtonDownLED = layer.ButtonDownLED != null ? XButtonLEDModel.FromDataLayer(layer.ButtonDownLED) : null,
            InitOSCCommands = new ObservableCollection<XOSCCommandModel>(layer.InitOSCCommmands.Select(XOSCCommandModel.FromDataLayer)),
            InitInternalCommands = new ObservableCollection<XInternalCommandModel>(layer.InitInternalCommands.Select(XInternalCommandModel.FromDataLayer)),
            InitMIDICommands = new ObservableCollection<XMIDICommandModel>(layer.InitMIDICommands.Select(XMIDICommandModel.FromDataLayer)),
            UnInitOscCommands = new ObservableCollection<XOSCCommandModel>(layer.UnInitOSCCommands.Select(XOSCCommandModel.FromDataLayer)),
            UnInitInternalCommands = new ObservableCollection<XInternalCommandModel>(layer.UnInitInternalCommands.Select(XInternalCommandModel.FromDataLayer)),
            UnInitMIDICommands = new ObservableCollection<XMIDICommandModel>(layer.UnInitMIDICommands.Select(XMIDICommandModel.FromDataLayer))
        };
    }
}