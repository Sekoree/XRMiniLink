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

public partial class XFaderModel : ViewModelBase
{
    [ObservableProperty]
    private XTouchMiniNotes _midiNote; //channel
    
    [ObservableProperty]
    private ObservableCollection<XTouchMiniNotes> _availableMidiNotes = new(Enum.GetValues<XTouchMiniNotes>());
    
    [ObservableProperty]
    private int _valueMin;
    
    [ObservableProperty]
    private int _valueMax;
    
    [ObservableProperty]
    private ObservableCollection<XOSCCommandModel> _oscCommands = new();
    
    [ObservableProperty]
    private XOSCCommandModel? _selectedOSCCommand;
    
    [ObservableProperty]
    private ObservableCollection<XInternalCommandModel> _internalCommands = new();
    
    [ObservableProperty]
    private XInternalCommandModel? _selectedInternalCommand;
    
    [ObservableProperty]
    private ObservableCollection<XMIDICommandModel> _midiCommands = new();
    
    [ObservableProperty]
    private XMIDICommandModel? _selectedMIDICommand;
    
    [RelayCommand]
    private void AddOSCCommand()
    {
        OscCommands.Add(new XOSCCommandModel());
    }

    [RelayCommand]
    private void RemoveOSCCommand()
    {
        if (SelectedOSCCommand == null) 
            return;
        OscCommands.Remove(SelectedOSCCommand);
        SelectedOSCCommand = null;
    }
    
    [RelayCommand]
    private void AddInternalCommand()
    {
        InternalCommands.Add(new XInternalCommandModel());
    }
    
    [RelayCommand]
    private void RemoveInternalCommand()
    {
        if (SelectedInternalCommand == null) 
            return;
        InternalCommands.Remove(SelectedInternalCommand);
        SelectedInternalCommand = null;
    }
    
    [RelayCommand]
    private void AddMIDICommand()
    {
        MidiCommands.Add(new XMIDICommandModel());
    }
    
    [RelayCommand]
    private void RemoveMIDICommand()
    {
        if (SelectedMIDICommand == null) 
            return;
        MidiCommands.Remove(SelectedMIDICommand);
        SelectedMIDICommand = null;
    }
    
    public XFader ToDataLayer()
    {
        return new XFader
        {
            MIDINote = (int)MidiNote,
            ValueMin = ValueMin,
            ValueMax = ValueMax,
            OSCCommands = [..OscCommands.Select(c => c.ToDataLayer())],
            InternalCommands = [..InternalCommands.Select(c => c.ToDataLayer())],
            MIDICommands = [..MidiCommands.Select(c => c.ToDataLayer())]
        };
    }
    
    public static XFaderModel FromDataLayer(XFader fader)
    {
        return new XFaderModel
        {
            MidiNote = (XTouchMiniNotes)fader.MIDINote,
            ValueMin = fader.ValueMin,
            ValueMax = fader.ValueMax,
            OscCommands = new ObservableCollection<XOSCCommandModel>(fader.OSCCommands.Select(XOSCCommandModel.FromDataLayer)),
            InternalCommands = new ObservableCollection<XInternalCommandModel>(fader.InternalCommands.Select(XInternalCommandModel.FromDataLayer)),
            MidiCommands = new ObservableCollection<XMIDICommandModel>(fader.MIDICommands.Select(XMIDICommandModel.FromDataLayer))
        };
    }
}