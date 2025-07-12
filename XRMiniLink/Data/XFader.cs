using LinkUITest;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;

namespace XRMiniLink.Data;

public class XFader : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;

    public string MidiNote { get; set; }
    public DataDirection Direction { get; set; } = DataDirection.Input;
    public float ValueMin { get; set; }
    public float ValueMax { get; set; }
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