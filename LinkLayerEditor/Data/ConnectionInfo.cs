namespace LinkLayerEditor.Data;

public class ConnectionInfo
{
    public string? RemoteAddress { get; set; }
    public int LocalPort { get; set; }
    public string? MIDIInput { get; set; }
    public string? MIDIOutput { get; set; }
}