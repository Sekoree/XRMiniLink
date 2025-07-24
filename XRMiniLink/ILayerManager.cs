using System.Collections.Concurrent;
using OscCore;
using XRMiniLink.Data;

namespace XRMiniLink;

public interface ILayerManager
{
    public ConcurrentDictionary<string, OscMessageRaw> SavedOSCValues { get; set; }
    public ConcurrentBag<XLayer> Layers { get; set; }
    public ConcurrentStack<XLayer> LayerHistory { get; set; }
    public XLayer CurrentLayer { get; set; }
    public void SwitchLayer(XLayer layer);
}