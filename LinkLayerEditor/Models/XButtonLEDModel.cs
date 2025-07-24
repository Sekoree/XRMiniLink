using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XButtonLEDModel : ViewModelBase
{
    [ObservableProperty]
    private XTouchMiniNotes _midiNote;
    
    [ObservableProperty]
    private ObservableCollection<XTouchMiniNotes> _availableMidiNotes = new(Enum.GetValues<XTouchMiniNotes>());
    
    [ObservableProperty]
    private bool _hasOSCCommand;
    
    [ObservableProperty]
    private XOSCCommandModel? _oscCommand;
    
    [ObservableProperty]
    private bool _hasInternalCommand;
    
    [ObservableProperty]
    private XInternalCommandModel? _internalCommand;
    
    [ObservableProperty]
    private bool _hasMIDICommand;
    
    [ObservableProperty]
    private XMIDICommandModel? _midiCommand;
    
    partial void OnHasOSCCommandChanged(bool value)
    {
        OscCommand = value switch
        {
            true when OscCommand == null => new XOSCCommandModel(),
            false => null,
            _ => OscCommand
        };
    }
    
    partial void OnHasInternalCommandChanged(bool value)
    {
        InternalCommand = value switch
        {
            true when InternalCommand == null => new XInternalCommandModel(),
            false => null,
            _ => InternalCommand
        };
    }
    
    partial void OnHasMIDICommandChanged(bool value)
    {
        MidiCommand = value switch
        {
            true when MidiCommand == null => new XMIDICommandModel(),
            false => null,
            _ => MidiCommand
        };
    }
    
    public XButtonLED ToDataLayer()
    {
        return new XButtonLED
        {
            MIDINote = (int)MidiNote,
            OSCCommand = HasOSCCommand ? OscCommand?.ToDataLayer() : null,
            InternalCommand = HasInternalCommand ? InternalCommand?.ToDataLayer() : null,
            MIDICommand = HasMIDICommand ? MidiCommand?.ToDataLayer() : null
        };
    }
    
    public static XButtonLEDModel FromDataLayer(XButtonLED buttonLED)
    {
        return new XButtonLEDModel
        {
            MidiNote = (XTouchMiniNotes)buttonLED.MIDINote,
            HasOSCCommand = buttonLED.OSCCommand != null,
            OscCommand = buttonLED.OSCCommand != null ? XOSCCommandModel.FromDataLayer(buttonLED.OSCCommand) : null,
            HasInternalCommand = buttonLED.InternalCommand != null,
            InternalCommand = buttonLED.InternalCommand != null ? XInternalCommandModel.FromDataLayer(buttonLED.InternalCommand) : null,
            HasMIDICommand = buttonLED.MIDICommand != null,
            MidiCommand = buttonLED.MIDICommand != null ? XMIDICommandModel.FromDataLayer(buttonLED.MIDICommand) : null
        };
    }
}