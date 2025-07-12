namespace XRMiniLink.Data;

public class XFader
{
    public string Name { get; set; }
    public DataDirection Direction { get; set; } = DataDirection.Input;
    public float ValueMin { get; set; }
    public float ValueMax { get; set; }
    public XCommand? Command { get; set; }
}