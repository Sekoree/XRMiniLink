using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XCommand
{
    public string Command { get; set; }
    public OscToken OSCType { get; set; }
    public float? Min { get; set; }
    public float? Max { get; set; }
}