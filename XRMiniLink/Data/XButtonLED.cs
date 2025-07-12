namespace XRMiniLink.Data;

public class XButtonLED
{
    public string MidiNote { get; set; }
    public DataDirection Direction => DataDirection.Output;
    /// <summary>
    /// 0 = Off, 1 = Blinking, 127 = On
    /// </summary>
    public XCommand? Command { get; set; }
}