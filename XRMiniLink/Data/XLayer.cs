using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;

namespace XRMiniLink.Data;

public class XLayer : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;
    
    public string Name { get; set; }
    public bool IsDefault { get; set; }

    public List<XBand> Bands { get; set; } = new();

    public XFader? Fader { get; set; }

    public XButton? ButtonUp { get; set; }
    public XButtonLED? ButtonUpLED { get; set; }
    
    public XButton? ButtonDown { get; set; }
    public XButtonLED? ButtonDownLED { get; set; }

    public List<XOSCCommand> InitOSCCommmands { get; set; } = new();
    public List<XInternalCommand> InitInternalCommands { get; set; } = new();
    public List<XMIDICommand> InitMIDICommands { get; set; } = new();
    
    public List<XOSCCommand> UnInitOSCCommands { get; set; } = new();
    public List<XInternalCommand> UnInitInternalCommands { get; set; } = new();
    public List<XMIDICommand> UnInitMIDICommands { get; set; } = new();
    
    
    public void InitLink(ILayerManager layerManager, SimpleOSC osc, InputDevice midiInput, OutputDevice midiOutput)
    {
        _layerManager = layerManager;
        _osc = osc;
        _midiInput = midiInput;
        _midiOutput = midiOutput;
        
        //enable all properies
        foreach (var band in Bands) 
            band.InitLink(layerManager, osc, midiInput, midiOutput);
        Fader?.InitLink(layerManager, osc, midiInput, midiOutput);
        ButtonUp?.InitLink(layerManager, osc, midiInput, midiOutput);
        ButtonUpLED?.InitLink(layerManager, osc, midiInput, midiOutput);
        ButtonDown?.InitLink(layerManager, osc, midiInput, midiOutput);
        ButtonDownLED?.InitLink(layerManager, osc, midiInput, midiOutput);
        
        // Run initial commands
        foreach (var command in InitOSCCommmands)
        {
            //OSC comms
        }
        
        foreach (var command in InitInternalCommands)
        {
            //Internal commands
        }
        
        foreach (var command in InitMIDICommands)
        {
            if (command.MidiType == MidiEventType.NoteOn)
            {
                if (_midiOutput == null) 
                    continue;
                
                var value = (SevenBitNumber)command.Value;
                var noteOnEvent = new NoteOnEvent((SevenBitNumber)command.MIDINote, value);
                _midiOutput.SendEvent(noteOnEvent);
            }
        }
    }

    public void DisableLink()
    {
        // Disable all properties
        foreach (var band in Bands) 
            band.DisableLink();
        Fader?.DisableLink();
        ButtonUp?.DisableLink();
        ButtonUpLED?.DisableLink();
        ButtonDown?.DisableLink();
        ButtonDownLED?.DisableLink();
        
        // Run uninitialization commands
        foreach (var command in UnInitOSCCommands)
        {
            //OSC comms
        }
        foreach (var command in UnInitInternalCommands)
        {
            //Internal commands
        }
        foreach (var command in UnInitMIDICommands)
        {
            if (command.MidiType == MidiEventType.NoteOn)
            {
                if (_midiOutput == null) 
                    continue;
                
                var value = (SevenBitNumber)command.Value;
                var noteOffEvent = new NoteOnEvent((SevenBitNumber)command.MIDINote, value);
                _midiOutput.SendEvent(noteOffEvent);
            }
        }
        
        _layerManager = null;
        _osc = null;
        _midiInput = null;
        _midiOutput = null;
    }

    public void UpdateValues()
    {
        foreach (var band in Bands)
            band.UpdateValues();
        Fader?.UpdateValues();
        ButtonUp?.UpdateValues();
        ButtonUpLED?.UpdateValues();
        ButtonDown?.UpdateValues();
        ButtonDownLED?.UpdateValues();
    }
}