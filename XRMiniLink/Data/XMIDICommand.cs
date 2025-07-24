using Melanchall.DryWetMidi.Core;

namespace XRMiniLink.Data;

public class XMIDICommand
{
    public int MIDINote { get; set; }
    public MidiEventType MidiType { get; set; }
    public int Value { get; set; }
}