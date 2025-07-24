using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;

namespace XRMiniLink.Data;

public class XFader : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;

    public int MIDINote { get; set; }
    public DataDirection Direction { get; set; } = DataDirection.Input;
    public int ValueMin { get; set; }
    public int ValueMax { get; set; }
    public List<XOSCCommand> OSCCommands { get; set; } = new();
    public List<XInternalCommand> InternalCommands { get; set; } = new();
    public List<XMIDICommand> MIDICommands { get; set; } = new();
    
    public void InitLink(ILayerManager layerManager, SimpleOSC osc, InputDevice midiInput, OutputDevice midiOutput)
    {
        _layerManager = layerManager;
        _osc = osc;
        _osc.MessageReceived += OscOnMessageReceived;
        _midiInput = midiInput;
        _midiInput.EventReceived += MidiInputOnEventReceived;
        _midiOutput = midiOutput;
    }

    private void MidiInputOnEventReceived(object? sender, MidiEventReceivedEventArgs e)
    {
        //fader is pitchwheel, midinote is channel
        if (e.Event is PitchBendEvent pbe)
        {
            if (pbe.Channel != MIDINote)
                return;

            foreach (var command in OSCCommands)
            {
                if (_layerManager == null)
                    continue;
                
                var commandAddress = command.Command;
                //pitchbend value is between 0 and 16256, scale it to ValueMin and ValueMax
                var value = 1.0f / 16256f;
                value = value * pbe.PitchValue;
                
                _osc?.SendOSCMessage(new OscMessage(commandAddress, value));
                _osc?.TriggerValue(new OscMessage(commandAddress));
            }
        }
    }

    private void OscOnMessageReceived(OscMessageRaw msg)
    {
    }

    public void DisableLink()
    {
        if (_osc != null)
        {
            _osc.MessageReceived -= OscOnMessageReceived;
            _osc = null;
        }
        if (_midiInput != null)
        {
            _midiInput.EventReceived -= MidiInputOnEventReceived;
            _midiInput = null;
        }
        _midiOutput = null;
        _layerManager = null;
    }

    public void UpdateValues()
    {
        //I guess needed?
        if (_layerManager == null || _osc == null)
            return;
        
        foreach (var command in OSCCommands)
        {
            var commandAddress = command.Command;
            _osc.TriggerValue(new OscMessage(commandAddress));
        }
    }
}