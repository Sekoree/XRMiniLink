using LinkUITest;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XButton : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;

    public string MidiNote { get; set; }
    public DataDirection Direction => DataDirection.Input;
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

    private void OscOnMessageReceived(OscMessageRaw msg)
    {
    }

    private void MidiInputOnEventReceived(object? sender, MidiEventReceivedEventArgs e)
    {
        if (e.Event is not NoteOnEvent noteOn || noteOn.Velocity <= 0) 
            return;
        var midiNote = SevenBitNumber.Parse(MidiNote);
        if (noteOn.NoteNumber != midiNote) 
            return;
        
        foreach (var command in InternalCommands)
        {
            if (command.Command == InternalCommand.SwitchLayer)
            {
                Console.WriteLine("##### Switch Layer Command Received #####");
                if (command.Parameters == null) 
                    continue;
                
                var layer = _layerManager?.Layers.GetValueOrDefault(command.Parameters);
                if (layer == null) 
                    continue;
                
                _layerManager?.SwitchLayer(layer);
            }
        }
        
        
        foreach (var command in OSCCommands)
        {
            if (command.IsToggle == true)
            {
                //try get the current value from the layer manager
                var currentValue = _layerManager?.SavedOSCValues.GetValueOrDefault(command.Command);
                if (currentValue == null)
                {
                    Console.WriteLine($"## OSC Command {command.Command} not cached, cant toggle ##");
                    continue;
                }

                if (command.OSCType != currentValue.Value[0].Type)
                {
                    Console.WriteLine("## OSC Command type mismatch, cant toggle ##");
                    continue;
                }

                if (command.OSCType == OscToken.Int)
                {
                    if (command.Min == null || command.Max == null)
                    {
                        Console.WriteLine("## OSC Command Min/Max not set, cant toggle ##");
                        continue;
                    }
                    
                    var oldValueArg = currentValue.Value[0];
                    var oldValue = currentValue.Value.ReadInt(ref oldValueArg);
                    
                    var valueToWrite = oldValue == (int)command.Min ? (int)command.Max : (int)command.Min;
                    
                    _osc?.SendOSCMessage(new OscMessage(command.Command, valueToWrite));
                    _osc?.TriggerValue(new OscMessage(command.Command));
                }
            }
            else
            {
                if (command.Value != null)
                {
                    if (command.OSCType == OscToken.Int)
                    {
                        var valueToWrite = int.Parse(command.Value);
                        _osc?.SendOSCMessage(new OscMessage(command.Command, valueToWrite));
                        _osc?.TriggerValue(new OscMessage(command.Command));
                    }
                    else
                    {
                        Console.WriteLine("## OSC Command type not supported for writing ##");
                    }
                }
                else
                {
                    Console.WriteLine("## OSC Command Value not set, cant write ##");
                }
            }
        }
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