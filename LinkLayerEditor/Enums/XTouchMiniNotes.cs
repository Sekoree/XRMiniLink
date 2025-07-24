namespace LinkLayerEditor.Enums;

public static class XTouchMiniConstants
{
    public static readonly int[] LEDRings = { 48, 49, 50, 51, 52, 53, 54, 55 };
    public static readonly int[] EncoderTurns = { 16, 17, 18, 19, 20, 21, 22, 23 };
    public static readonly int[] EncoderPresses = { 32, 33, 34, 35, 36, 37, 38, 39 };
    public static readonly int[] BandButtonsRow1 = { 89, 90, 40, 41, 42, 43, 44, 45 };
    public static readonly int[] BandButtonsRow2 = { 87, 88, 91, 92, 86, 93, 94, 95 };
}

public enum XTouchMiniNotes
{
    //LED Rings
    LEDRing1 = 48,
    LEDRing2 = 49,
    LEDRing3 = 50,
    LEDRing4 = 51,
    LEDRing5 = 52,
    LEDRing6 = 53,
    LEDRing7 = 54,
    LEDRing8 = 55,
    
    //Encoders
    Encoder1Turn = 16,
    Encoder1Press = 32,
    Encoder2Turn = 17,
    Encoder2Press = 33,
    Encoder3Turn = 18,
    Encoder3Press = 34,
    Encoder4Turn = 19,
    Encoder4Press = 35,
    Encoder5Turn = 20,
    Encoder5Press = 36,
    Encoder6Turn = 21,
    Encoder6Press = 37,
    Encoder7Turn = 22,
    Encoder7Press = 38,
    Encoder8Turn = 23,
    Encoder8Press = 39,
    
    //Band Buttons Row 1
    Button1 = 89,
    Button2 = 90,
    Button3 = 40,
    Button4 = 41,
    Button5 = 42,
    Button6 = 43,
    Button7 = 44,
    Button8 = 45,
    
    //Band Buttons Row 2
    Button9 = 87,
    Button10 = 88,
    Button11 = 91,
    Button12 = 92,
    Button13 = 86,
    Button14 = 93,
    Button15 = 94,
    Button16 = 95,
    
    //Fader
    Fader = 8, //Channel Number (Pitch Bend)
    
    //Side Buttons
    ButtonLayerUp = 84,
    ButtonLayerDown = 85,
}