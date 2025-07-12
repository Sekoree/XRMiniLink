namespace XRMiniLink.Data;

public class XLayer
{
    public string Name { get; set; }
    public bool IsDefault { get; set; }
    
    public List<XBand> Bands { get; set; } = new();
    
    public XFader? Fader { get; set; }
    
    public XCommand? ButtonUp { get; set; } 
    public XCommand? ButtonDown { get; set; } 
}