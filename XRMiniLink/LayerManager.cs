using System.Collections.Concurrent;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using XRMiniLink.Data;

namespace XRMiniLink;

public class LayerManager : ILayerManager
{
    private readonly SimpleOSC _osc;
    private readonly InputDevice _midiInput;
    private readonly OutputDevice _midiOutput;

    public ConcurrentDictionary<string, OscMessageRaw> SavedOSCValues { get; set; } = new();

    public ConcurrentBag<XLayer> Layers { get; set; } = new();
    public ConcurrentStack<XLayer> LayerHistory { get; set; } = new();
    public XLayer? CurrentLayer { get; set; }
    
    public LayerManager(SimpleOSC osc, InputDevice midiInput, OutputDevice midiOutput)
    {
        _osc = osc;
        _midiInput = midiInput;
        _midiOutput = midiOutput;
        _osc.MessageReceived += OscOnMessageReceived;
    }

    private void OscOnMessageReceived(OscMessageRaw obj)
    {
        if (SavedOSCValues.ContainsKey(obj.Address))
            SavedOSCValues[obj.Address] = obj;
        else
            SavedOSCValues.TryAdd(obj.Address, obj);
    }

    public void SwitchLayer(XLayer layer)
    {
        if (CurrentLayer != null)
        {
            // Disable current layer
            CurrentLayer.DisableLink();
            LayerHistory.Push(CurrentLayer);
        }
        CurrentLayer = layer;

        if (CurrentLayer != null)
        {
            // Enable new layer
            CurrentLayer.InitLink(this, _osc, _midiInput, _midiOutput);
            CurrentLayer.UpdateValues();
        }
    }
}