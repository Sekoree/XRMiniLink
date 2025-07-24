using System.Text.Json.Serialization;
using Melanchall.DryWetMidi.Multimedia;
using XRMiniLink.Data;

namespace XRMiniLink;

public interface ILinkHandler
{
    public void InitLink(ILayerManager layerManager, SimpleOSC osc, InputDevice midiInput, OutputDevice midiOutput);
    public void DisableLink();
    public void UpdateValues();
}