
using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XLEDButton
{
    public string MidiNote { get; set; }
    public string MidiLEDNote { get; set; }
    public DataDirection Direction => DataDirection.Both;
    public XCommand? PressCommand { get; set; }
    /// <summary>
    /// 0 = Off, 1 = Blinking, 127 = On
    /// </summary>
    public XCommand? LEDStatusCommand { get; set; }
}