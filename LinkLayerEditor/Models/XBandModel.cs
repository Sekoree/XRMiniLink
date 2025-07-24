using CommunityToolkit.Mvvm.ComponentModel;
using LinkLayerEditor.Enums;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XBandModel : ViewModelBase
{
    [ObservableProperty] private int _index;

    [ObservableProperty] private bool _hasLEDRing;

    [ObservableProperty] private XLEDRingModel? _ledRing;

    [ObservableProperty] private bool _hasEncoder;

    [ObservableProperty] private XEncoderModel? _encoder;

    [ObservableProperty] private bool _hasEncoderButton;

    [ObservableProperty] private XButtonModel? _encoderButton;

    [ObservableProperty] private bool _hasTopButton;

    [ObservableProperty] private XButtonModel? _topButton;

    [ObservableProperty] private bool _hasTopButtonLED;

    [ObservableProperty] private XButtonLEDModel? _topButtonLED;

    [ObservableProperty] private bool _hasBottomButton;

    [ObservableProperty] private XButtonModel? _bottomButton;

    [ObservableProperty] private bool _hasBottomButtonLED;

    [ObservableProperty] private XButtonLEDModel? _bottomButtonLED;

    public XBandModel(int index)
    {
        Index = index;
    }

    partial void OnHasLEDRingChanged(bool value)
    {
        LedRing = value switch
        {
            true when LedRing == null => new XLEDRingModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.LEDRings[Index - 1]
            },
            false => null,
            _ => LedRing
        };
    }

    partial void OnHasEncoderChanged(bool value)
    {
        Encoder = value switch
        {
            true when Encoder == null => new XEncoderModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.EncoderTurns[Index - 1]
            },
            false => null,
            _ => Encoder
        };
    }

    partial void OnHasEncoderButtonChanged(bool value)
    {
        EncoderButton = value switch
        {
            true when EncoderButton == null => new XButtonModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.EncoderPresses[Index - 1]
            },
            false => null,
            _ => EncoderButton
        };
    }

    partial void OnHasTopButtonChanged(bool value)
    {
        TopButton = value switch
        {
            true when TopButton == null => new XButtonModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.BandButtonsRow1[Index - 1]
            },
            false => null,
            _ => TopButton
        };
    }

    partial void OnHasTopButtonLEDChanged(bool value)
    {
        TopButtonLED = value switch
        {
            true when TopButtonLED == null => new XButtonLEDModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.BandButtonsRow1[Index - 1]
            },
            false => null,
            _ => TopButtonLED
        };
    }

    partial void OnHasBottomButtonChanged(bool value)
    {
        BottomButton = value switch
        {
            true when BottomButton == null => new XButtonModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.BandButtonsRow2[Index - 1]
            },
            false => null,
            _ => BottomButton
        };
    }

    partial void OnHasBottomButtonLEDChanged(bool value)
    {
        BottomButtonLED = value switch
        {
            true when BottomButtonLED == null => new XButtonLEDModel()
            {
                MidiNote = (XTouchMiniNotes)XTouchMiniConstants.BandButtonsRow2[Index - 1]
            },
            false => null,
            _ => BottomButtonLED
        };
    }

    public XBand ToDataLayer()
    {
        return new XBand
        {
            LEDRing = HasLEDRing ? LedRing?.ToDataLayer() : null,
            Encoder = HasEncoder ? Encoder?.ToDataLayer() : null,
            EncoderButton = HasEncoderButton ? EncoderButton?.ToDataLayer() : null,
            TopButton = HasTopButton ? TopButton?.ToDataLayer() : null,
            TopButtonLED = HasTopButtonLED ? TopButtonLED?.ToDataLayer() : null,
            BottomButton = HasBottomButton ? BottomButton?.ToDataLayer() : null,
            BottomButtonLED = HasBottomButtonLED ? BottomButtonLED?.ToDataLayer() : null
        };
    }

    public static XBandModel FromDataLayer(XBand band, int index)
    {
        return new XBandModel(index)
        {
            HasLEDRing = band.LEDRing != null,
            LedRing = band.LEDRing != null ? XLEDRingModel.FromDataLayer(band.LEDRing) : null,
            HasEncoder = band.Encoder != null,
            Encoder = band.Encoder != null ? XEncoderModel.FromDataLayer(band.Encoder) : null,
            HasEncoderButton = band.EncoderButton != null,
            EncoderButton = band.EncoderButton != null ? XButtonModel.FromDataLayer(band.EncoderButton) : null,
            HasTopButton = band.TopButton != null,
            TopButton = band.TopButton != null ? XButtonModel.FromDataLayer(band.TopButton) : null,
            HasTopButtonLED = band.TopButtonLED != null,
            TopButtonLED = band.TopButtonLED != null ? XButtonLEDModel.FromDataLayer(band.TopButtonLED) : null,
            HasBottomButton = band.BottomButton != null,
            BottomButton = band.BottomButton != null ? XButtonModel.FromDataLayer(band.BottomButton) : null,
            HasBottomButtonLED = band.BottomButtonLED != null,
            BottomButtonLED = band.BottomButtonLED != null ? XButtonLEDModel.FromDataLayer(band.BottomButtonLED) : null
        };
    }
}