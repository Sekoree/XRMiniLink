using LinkUITest;
using Melanchall.DryWetMidi.Multimedia;

namespace XRMiniLink.Data;

public class XBand : ILinkHandler
{
    private ILayerManager? _layerManager;
    private SimpleOSC? _osc;
    private InputDevice? _midiInput;
    private OutputDevice? _midiOutput;
    
    public XLEDRing? LEDRing { get; set; }

    public XEncoder? Encoder { get; set; }
    public XButton? EncoderButton { get; set; }
    
    public XButton? TopButton { get; set; }
    public XButtonLED? TopButtonLED { get; set; }
    public XButton? BottomButton { get; set; }
    public XButtonLED? BottomButtonLED { get; set; }

    public void InitLink(ILayerManager layerManager, SimpleOSC osc, InputDevice midiInput, OutputDevice midiOutput)
    {
        _layerManager = layerManager;
        _osc = osc;
        _midiInput = midiInput;
        _midiOutput = midiOutput;
        
        // Enable all properties
        LEDRing?.InitLink(layerManager, osc, midiInput, midiOutput);
        Encoder?.InitLink(layerManager, osc, midiInput, midiOutput);
        EncoderButton?.InitLink(layerManager, osc, midiInput, midiOutput);
        TopButton?.InitLink(layerManager, osc, midiInput, midiOutput);
        TopButtonLED?.InitLink(layerManager, osc, midiInput, midiOutput);
        BottomButton?.InitLink(layerManager, osc, midiInput, midiOutput);
        BottomButtonLED?.InitLink(layerManager, osc, midiInput, midiOutput);
    }

    public void DisableLink()
    {
        _layerManager = null;
        _osc = null;
        _midiInput = null;
        _midiOutput = null;
        
        // Disable all properties
        LEDRing?.DisableLink();
        Encoder?.DisableLink();
        EncoderButton?.DisableLink();
        TopButton?.DisableLink();
        TopButtonLED?.DisableLink();
        BottomButton?.DisableLink();
        BottomButtonLED?.DisableLink();
    }

    public void UpdateValues()
    {
        LEDRing?.UpdateValues();
        Encoder?.UpdateValues();
        EncoderButton?.UpdateValues();
        TopButton?.UpdateValues();
        TopButtonLED?.UpdateValues();
        BottomButton?.UpdateValues();
        BottomButtonLED?.UpdateValues();
    }
}