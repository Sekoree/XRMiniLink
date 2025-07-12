using System.Collections.Concurrent;
using OscCore;
using XRMiniLink.Data;

namespace LinkUITest;

public interface ILayerManager
{
    public ConcurrentDictionary<string, OscMessageRaw> SavedOSCValues { get; set; }
    public ConcurrentDictionary<string, XLayer> Layers { get; set; }
    public void SwitchLayer(XLayer layer);
}