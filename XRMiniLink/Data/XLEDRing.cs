using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XLEDRing : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;
    
    public int MIDINote { get; set; }
    public DataDirection Direction => DataDirection.Output;
    /// <summary>
    /// Hardcoded for Fan (33-43) for now.
    /// 0 = Off, 1-11 Single LED, 17-27 = Pan, 49-59 = Spread
    /// </summary>
    public float ValueMin => 32;
    public float ValueMax => 43;
    public XOSCCommand? OSCCommand { get; set; }
    public XInternalCommand? InternalCommand { get; set; }
    public XMIDICommand? MIDICommand { get; set; }
    
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
    }

    private void OscOnMessageReceived(OscMessageRaw msg)
    {
        if (OSCCommand == null || _layerManager == null)
            return;
        
        var commandAddress = OSCCommand.Command;
        if (msg.Address == commandAddress)
        {
            var arg0 = msg[0];
            if (arg0.Type == OscToken.Float)
            {
                if (OSCCommand == null || OSCCommand.Min == null || OSCCommand.Max == null)
                {
                    Console.WriteLine($"XLEDRing: Command {commandAddress} is not properly configured with Min/Max values.");
                    return;
                }
                var commandValRange = OSCCommand.Max.Value - OSCCommand.Min.Value;
                var value = msg.ReadFloat(ref arg0);
                if (value < OSCCommand.Min || value > OSCCommand.Max)
                {
                    Console.WriteLine($"XLEDRing: Value {value} out of range ({OSCCommand.Min}-{OSCCommand.Max}) for command {commandAddress}");
                    return;
                }
                //led ring values are basically 0-11, scale value to that range
                var scaledValue = (int)((value - OSCCommand.Min.Value) / commandValRange * (ValueMax - ValueMin));
                if (_midiOutput == null) 
                    return;
                // Send MIDI Note On for the LED ring
                var midiVal = ValueMin + scaledValue; // 33 is the first LED note
                _midiOutput.SendEvent(new ControlChangeEvent((SevenBitNumber)MIDINote, (SevenBitNumber)midiVal));
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
        if (OSCCommand != null)
            _osc?.TriggerValue(new OscMessage(OSCCommand.Command));
    }
}