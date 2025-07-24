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

    public int MIDINote { get; set; }
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
        if (noteOn.NoteNumber != MIDINote) 
            return;
        if (_layerManager == null)
            return;
        
        foreach (var command in InternalCommands)
        {
            if (command.Command == InternalCommand.SwitchLayer)
            {
                if (command.Parameters == null) 
                    continue;
                
                var layer = _layerManager?.Layers.FirstOrDefault(x => x.Name == command.Parameters);
                if (layer == null)
                {
                    Console.WriteLine($"Could not find layer: {command.Parameters}");
                    continue;
                }
                
                var currentLayer = _layerManager?.CurrentLayer;
                if (currentLayer == null)
                    continue;

                _layerManager?.SwitchLayer(layer);
            }
            else if (command.Command == InternalCommand.GoBack)
            {
                XLayer? lastLayer = null;
                var hasLastLayer = _layerManager?.LayerHistory.TryPop(out lastLayer);
                if (hasLastLayer != true || lastLayer == null)
                {
                    Console.WriteLine($"Could not find layer: {command.Parameters}");
                    continue;
                }
                
                _layerManager?.SwitchLayer(lastLayer);
            }
        }
        
        
        foreach (var command in OSCCommands)
        {
            if (command.IsToggle)
            {
                var commandAddress = command.Command;
                //try get the current value from the layer manager
                OscMessageRaw currentCommandValue = default;
                if (_layerManager?.SavedOSCValues.TryGetValue(commandAddress, out currentCommandValue) != true)
                {
                    Console.WriteLine($"## OSC Command {commandAddress} not cached, cant toggle ##");
                    continue;
                }
                
                if (currentCommandValue.Count == 0)
                {
                    Console.WriteLine($"## OSC Command {commandAddress} has no value, cant toggle ##");
                    continue;
                }

                if (command.OSCType != currentCommandValue[0].Type)
                {
                    Console.WriteLine("## OSC Command type mismatch, cant toggle ##");
                    continue;
                }

                if (command.OSCType == OscToken.Int)
                {
                    if (command.Min == null || command.Max == null)
                    {
                        //no value conversion, just send the command
                        
                        continue;
                    }
                    
                    var oldValueArg = currentCommandValue[0];
                    var oldValue = currentCommandValue.ReadInt(ref oldValueArg);
                    
                    var valueToWrite = oldValue == (int)command.Min ? (int)command.Max : (int)command.Min;
                    
                    _osc?.SendOSCMessage(new OscMessage(commandAddress, valueToWrite));
                    _osc?.TriggerValue(new OscMessage(commandAddress));
                }
                else
                {
                    Console.WriteLine("## OSC Command type not supported for toggling ##");
                }
            }
            else
            {
                var commandAddress = command.Command;
                if (command.Value != null)
                {
                    if (command.OSCType == OscToken.Int)
                    {
                        var valueToWrite = int.Parse(command.Value);
                        _osc?.SendOSCMessage(new OscMessage(commandAddress, valueToWrite));
                        _osc?.TriggerValue(new OscMessage(commandAddress));
                    }
                    else if (command.OSCType == OscToken.Float)
                    {
                        var valueToWrite = float.Parse(command.Value);
                        _osc?.SendOSCMessage(new OscMessage(commandAddress, valueToWrite));
                        _osc?.TriggerValue(new OscMessage(commandAddress));
                    }
                    else if (command.OSCType == OscToken.String)
                    {
                        _osc?.SendOSCMessage(new OscMessage(commandAddress, command.Value));
                        _osc?.TriggerValue(new OscMessage(commandAddress));
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