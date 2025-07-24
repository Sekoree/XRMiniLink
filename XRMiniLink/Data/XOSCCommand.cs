using OscCore.LowLevel;

namespace XRMiniLink.Data;

public class XOSCCommand
{
    public string Command { get; set; }
    public OscToken OSCType { get; set; }
    public bool IsToggle { get; set; }
    public string? Value { get; set; }
    public float? Min { get; set; }
    public float? Max { get; set; }
}