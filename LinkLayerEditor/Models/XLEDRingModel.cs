using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XLEDRingModel : ViewModelBase
{
    [ObservableProperty]
    private XTouchMiniNotes _midiNote;
    
    [ObservableProperty]
    private ObservableCollection<XTouchMiniNotes> _availableMidiNotes = new(Enum.GetValues<XTouchMiniNotes>());
    
    //[ObservableProperty]
    //private float _valueMin;
    //
    //[ObservableProperty]
    //private float _valueMax;
    
    [ObservableProperty]
    private bool _hasOscCommand;
    
    [ObservableProperty]
    private XOSCCommandModel? _oscCommand;
    
    [ObservableProperty]
    private bool _hasInternalCommand;
    
    [ObservableProperty]
    private XInternalCommandModel? _internalCommand;
    
    [ObservableProperty]
    private bool _hasMidiCommand;
    
    [ObservableProperty]
    private XMIDICommandModel? _midiCommand;

    partial void OnHasOscCommandChanged(bool value)
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
    
    partial void OnHasMidiCommandChanged(bool value)
    {
        MidiCommand = value switch
        {
            true when MidiCommand == null => new XMIDICommandModel(),
            false => null,
            _ => MidiCommand
        };
    }

    public XLEDRing ToDataLayer()
    {
        return new XLEDRing
        {
            MIDINote = (int)MidiNote,
            //ValueMin = ValueMin,
            //ValueMax = ValueMax,
            OSCCommand = OscCommand?.ToDataLayer(),
            InternalCommand = InternalCommand?.ToDataLayer(),
            MIDICommand = MidiCommand?.ToDataLayer()
        };
    }
    
    public static XLEDRingModel FromDataLayer(XLEDRing ring)
    {
        var model = new XLEDRingModel
        {
            MidiNote = (XTouchMiniNotes)ring.MIDINote,
            //ValueMin = ring.ValueMin,
            //ValueMax = ring.ValueMax,
            HasOscCommand = ring.OSCCommand != null,
            HasInternalCommand = ring.InternalCommand != null,
            HasMidiCommand = ring.MIDICommand != null,
        };
        model.HasOscCommand = ring.OSCCommand != null;
        model.OscCommand = ring.OSCCommand != null ? XOSCCommandModel.FromDataLayer(ring.OSCCommand) : null;
        model.HasInternalCommand = ring.InternalCommand != null;
        model.InternalCommand = ring.InternalCommand != null ? XInternalCommandModel.FromDataLayer(ring.InternalCommand) : null;
        model.HasMidiCommand = ring.MIDICommand != null;
        model.MidiCommand = ring.MIDICommand != null ? XMIDICommandModel.FromDataLayer(ring.MIDICommand) : null;
        return model;
    }
}