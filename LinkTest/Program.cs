using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using XRMiniLink;
using XRMiniLink.Data;

namespace LinkTest;

class Program
{
    //192.168.2.76
    private static XLayer _testLayer = TestLayer.GetTestLayer();
    private static XLayer _testLayer2 = TestLayer.GetTestLayer2();
    private static SimpleOSC _oscClient;
    private static OutputDevice _midiOutputDevice;
    
    //save precise value of OSC commands to avoid rounding errors from float to int conversion with MIDI
    private static Dictionary<string, float> _oscValues = new();
    
    static async Task Main(string[] args)
    {
        //var jsonOpts = new JsonSerializerOptions
        //{
        //    WriteIndented = true,
        //    DefaultIgnoreCondition = JsonIgnoreCondition.Never
        //};
        //var json = JsonSerializer.Serialize(_testLayer, jsonOpts);
        //await File.WriteAllTextAsync(Path.Combine(Directory.GetCurrentDirectory(), "test.json"), json);
        //Console.WriteLine("Test layer JSON saved to test.json");
        //return;
        
        var listener = new IPEndPoint(IPAddress.Any, 8002);
        //var receiver = new IPEndPoint(IPAddress.Parse("172.17.176.1"), 10023);
        var receiver = new IPEndPoint(IPAddress.Parse("192.168.2.76"), 10023);
        _oscClient = new SimpleOSC(listener, receiver);
        var midiInputDevice = InputDevice.GetByName("X-TOUCH MINI");
        midiInputDevice.EventReceived += (sender, e) =>
        {
            // Handle MIDI input events here
            if (e.Event is NoteOnEvent noteOnEvent)
            {
                Console.WriteLine($"MIDI Note On: {noteOnEvent.NoteNumber} Velocity: {noteOnEvent.Velocity}");
            }
            else if (e.Event is ControlChangeEvent controlChangeEvent)
            {
                Console.WriteLine($"MIDI Control Change: {controlChangeEvent.ControlNumber}, Value: {controlChangeEvent.ControlValue}");
            }
            else if (e.Event is PitchBendEvent pitchBendEvent)
            {
                Console.WriteLine($"MIDI Pitch Channel: {pitchBendEvent.Channel}, Value: {pitchBendEvent.PitchValue}");
            }
                
        };
        _midiOutputDevice = OutputDevice.GetByName("X-TOUCH MINI");
        midiInputDevice.StartEventsListening();
        _midiOutputDevice.PrepareForEventsSending();
        _oscClient.StartReceiving();
        
        var testLayer = _testLayer;
        var layerManager = new LayerManager(_oscClient, midiInputDevice, _midiOutputDevice)
        {
            Layers =
            {
                testLayer,
                _testLayer2
            },
            CurrentLayer = testLayer
        };
        testLayer.InitLink(layerManager, _oscClient, midiInputDevice, _midiOutputDevice);
        testLayer.UpdateValues();
        
        await Task.Delay(Timeout.Infinite);
    }
}