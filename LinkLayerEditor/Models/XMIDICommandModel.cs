using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using Melanchall.DryWetMidi.Core;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XMIDICommandModel : ViewModelBase
{
    [ObservableProperty] private XTouchMiniNotes _midiNote;
    
    [ObservableProperty] private ObservableCollection<XTouchMiniNotes> _availableMidiNotes = new(Enum.GetValues<XTouchMiniNotes>());

    [ObservableProperty] private int _value;

    [ObservableProperty] private MidiEventType _midiType;

    [ObservableProperty] private ObservableCollection<MidiEventType> _availableMidiTypes = new(Enum.GetValues<MidiEventType>());
    
    public XMIDICommand ToDataLayer()
    {
        return new XMIDICommand
        {
            MIDINote = (int)MidiNote,
            Value = Value,
            MidiType = MidiType
        };
    }
    
    public static XMIDICommandModel FromDataLayer(XMIDICommand command)
    {
        return new XMIDICommandModel
        {
            MidiNote = (XTouchMiniNotes)command.MIDINote,
            Value = command.Value,
            MidiType = command.MidiType
        };
    }
}