using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.Dialogs;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XButtonModel : ViewModelBase
{
    [ObservableProperty]
    private XTouchMiniNotes _midiNote;
    
    [ObservableProperty]
    private ObservableCollection<XTouchMiniNotes> _availableMidiNotes = new(Enum.GetValues<XTouchMiniNotes>());

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
    
    public XButton ToDataLayer()
    {
        return new XButton
        {
            MIDINote = (int)MidiNote,
            OSCCommands = OscCommands.Select(cmd => cmd.ToDataLayer()).ToList(),
            InternalCommands = InternalCommands.Select(cmd => cmd.ToDataLayer()).ToList(),
            MIDICommands = MidiCommands.Select(cmd => cmd.ToDataLayer()).ToList()
        };
    }
    
    public static XButtonModel FromDataLayer(XButton button)
    {
        return new XButtonModel
        {
            MidiNote = (XTouchMiniNotes)button.MIDINote,
            OscCommands = new ObservableCollection<XOSCCommandModel>(button.OSCCommands.Select(XOSCCommandModel.FromDataLayer)),
            InternalCommands = new ObservableCollection<XInternalCommandModel>(button.InternalCommands.Select(XInternalCommandModel.FromDataLayer)),
            MidiCommands = new ObservableCollection<XMIDICommandModel>(button.MIDICommands.Select(XMIDICommandModel.FromDataLayer))
        };
    }
}