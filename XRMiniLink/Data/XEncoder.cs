using LinkUITest;
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

    public string MidiNote { get; set; }
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
            return;
        var noteNumber = SevenBitNumber.Parse(MidiNote);
        if (cce.ControlNumber != noteNumber)
            return;

        foreach (var command in OSCCommands)
        {
            var currentCommandValue = _layerManager?.SavedOSCValues.GetValueOrDefault(command.Command);
            if (currentCommandValue == null)
            {
                Console.WriteLine($"No current value for command {command.Command}, skipping.");
                continue;
            }

            if (!float.TryParse(command.Value, out var minStep))
                continue;

            var arg0 = currentCommandValue.Value[0];
            if (arg0.Type == OscToken.Float)
            {
                var currentValue = currentCommandValue.Value.ReadFloat(ref arg0);
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
                var newOscMessage = new OscMessage(command.Command, newValue);
                _osc?.SendOSCMessage(newOscMessage);
                _osc?.TriggerValue(new OscMessage(command.Command));
            }
            else
            {
                Console.WriteLine($"Unsupported type for command {command.Command}, expected float.");
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