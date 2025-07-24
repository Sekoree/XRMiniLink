using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XButtonLED : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;

    public int MIDINote { get; set; }
    public DataDirection Direction => DataDirection.Output;

    /// <summary>
    /// 0 = Off, 1 = Blinking, 127 = On
    /// </summary>
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
        if (msg.Address != commandAddress)
            return;

        if (msg[0].Type != OSCCommand.OSCType)
        {
            Console.WriteLine("## OSC Command type mismatch, expected Int ##");
            return;
        }

        if (OSCCommand.OSCType == OscToken.Int)
        {
            var valueArg = msg[0];
            var value = msg.ReadInt(ref valueArg);
            
            if (OSCCommand.Min == null || OSCCommand.Max == null)
            {
                Console.WriteLine($"## OSC Command {commandAddress} has no Min/Max defined, cant write value ##");
                return;
            }
            
            var valueToWrite = value == 0 ? (int)OSCCommand.Min : (int)OSCCommand.Max;
            _midiOutput?.SendEvent(new NoteOnEvent((SevenBitNumber)MIDINote, (SevenBitNumber)valueToWrite));
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