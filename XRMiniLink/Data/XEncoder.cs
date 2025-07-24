using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XEncoder : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;

    public int MIDINote { get; set; }
    public DataDirection Direction => DataDirection.Input;
    public int DecrementStart => 65;
    public int IncrementStart => 1;
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
        if (e.Event is not ControlChangeEvent cce)
            return;;
        if (cce.ControlNumber != MIDINote)
            return;

        foreach (var command in OSCCommands)
        {
            if (_layerManager == null)
                continue;
                
            var commandAddress = command.Command;
            OscMessageRaw currentCommandValue = default;
            if (_layerManager?.SavedOSCValues.TryGetValue(commandAddress, out currentCommandValue) == false)
            {
                Console.WriteLine($"No current value for command {commandAddress}, skipping.");
                continue;
            }

            if (!float.TryParse(command.Value, out var minStep))
                continue;
            
            if (currentCommandValue.Count == 0)
            {
                Console.WriteLine($"No value found for command {commandAddress}, skipping.");
                continue;
            }

            var arg0 = currentCommandValue[0];
            if (arg0.Type == OscToken.Float)
            {
                var currentValue = currentCommandValue.ReadFloat(ref arg0);
                float newValue;
                var isDecrement = cce.ControlValue >= this.DecrementStart;
                if (isDecrement)
                {
                    // Decrement logic
                    var changeValue = (this.DecrementStart - (cce.ControlValue + 1)) * -1;
                    newValue = currentValue - (minStep * (changeValue * changeValue));
                }
                else
                {
                    // Increment logic
                    var changeValue = (cce.ControlValue + 1) - this.IncrementStart;
                    newValue = currentValue + (minStep * (changeValue * changeValue));
                }

                newValue = Math.Clamp(newValue, 0f, 1f); // Ensure the value is within [0, 1]
                var newOscMessage = new OscMessage(commandAddress, newValue);
                _osc?.SendOSCMessage(newOscMessage);
                _osc?.TriggerValue(new OscMessage(commandAddress));
            }
            else
            {
                Console.WriteLine($"Unsupported type for command {commandAddress}, expected float.");
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
    }
}