using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Avalonia.Threading;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.Data;
using LinkLayerEditor.Enums;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using OscCore;
using XRMiniLink;
using XRMiniLink.Data;

namespace LinkLayerEditor.ViewModels;

public partial class LiveLinkViewModel : ViewModelBase, ILayerManager, IDisposable
{
    public ConcurrentDictionary<string, OscMessageRaw> SavedOSCValues { get; set; } = new();
    public ConcurrentBag<XLayer> Layers { get; set; }
    public ConcurrentStack<XLayer> LayerHistory { get; set; } = new();

    [ObservableProperty] private XLayer _currentLayer;

    [ObservableProperty] private string? _remoteAddress;

    [ObservableProperty] private int? _localPort = 8002;

    [ObservableProperty] private ObservableCollection<InputDevice> _midiInputs = new(InputDevice.GetAll());

    [ObservableProperty] private ObservableCollection<OutputDevice> _midiOutputs = new(OutputDevice.GetAll());

    [ObservableProperty] private InputDevice? _selectedMidiInput;

    [ObservableProperty] private OutputDevice? _selectedMidiOutput;

    private SimpleOSC? _osc;

    [ObservableProperty] private bool _isConnected;

    [ObservableProperty] private bool _isLayerLive;


    public LiveLinkViewModel(List<XLayer> layers)
    {
        Layers = new ConcurrentBag<XLayer>(layers);
        CurrentLayer = Layers.Any(x => x.IsDefault) ? layers.First(x => x.IsDefault) : layers.First();

        _ = Task.Run(async () =>
        {
            if (!File.Exists("connection_info.json"))
                return;
            var json = await File.ReadAllTextAsync("connection_info.json");
            try
            {
                var info = System.Text.Json.JsonSerializer.Deserialize<ConnectionInfo>(json);
                if (info == null)
                    return;
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    RemoteAddress = info.RemoteAddress;
                    LocalPort = info.LocalPort;
                    SelectedMidiInput = MidiInputs.FirstOrDefault(x => x.Name == info.MIDIInput);
                    SelectedMidiOutput = MidiOutputs.FirstOrDefault(x => x.Name == info.MIDIOutput);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading connection info: {ex.Message}");
            }
        });
    }

    [RelayCommand]
    private async Task StartOSCConnection()
    {
        if (_osc != null)
        {
            _osc.Dispose();
            _osc.MessageReceived -= OscOnMessageReceived;
            _osc = null;
            IsConnected = false;
        }

        if (string.IsNullOrWhiteSpace(RemoteAddress) || LocalPort == null)
            return;

        _osc = new SimpleOSC(new IPEndPoint(IPAddress.Any, LocalPort.Value), IPEndPoint.Parse(RemoteAddress));
        _osc.MessageReceived += OscOnMessageReceived;
        _osc.StartReceiving();
        IsConnected = true;
        var info = new ConnectionInfo
        {
            RemoteAddress = RemoteAddress,
            LocalPort = LocalPort.Value,
            MIDIInput = SelectedMidiInput?.Name,
            MIDIOutput = SelectedMidiOutput?.Name
        };
        //save as JSON in current directory
        var json = System.Text.Json.JsonSerializer.Serialize(info);
        await File.WriteAllTextAsync("connection_info.json", json);
    }

    [RelayCommand]
    private void StopOSCConnection()
    {
        if (_osc == null)
            return;
        _osc.StopReceiving();
        _osc.MessageReceived -= OscOnMessageReceived;
        _osc.Dispose();
        _osc = null;
        IsConnected = false;
    }

    [RelayCommand]
    private void StartLayerLive()
    {
        if (_osc == null || SelectedMidiInput == null || SelectedMidiOutput == null)
            return;

        IsLayerLive = true;
        SelectedMidiInput.StartEventsListening();
        SelectedMidiOutput.PrepareForEventsSending();
        ClearLEDs();
        CurrentLayer.InitLink(this, _osc, SelectedMidiInput, SelectedMidiOutput);
        CurrentLayer.UpdateValues();
    }

    [RelayCommand]
    private void StopLayerLive()
    {
        IsLayerLive = false;
        SelectedMidiInput?.StopEventsListening();
        //SelectedMidiOutput?.TurnAllNotesOff();
        CurrentLayer.DisableLink();
    }

    [RelayCommand]
    private void RefreshMidiDevices()
    {
        MidiInputs = new ObservableCollection<InputDevice>(InputDevice.GetAll());
        MidiOutputs = new ObservableCollection<OutputDevice>(OutputDevice.GetAll());
    }

    partial void OnSelectedMidiInputChanged(InputDevice? value)
    {
        if (!IsLayerLive)
            return;
        StopLayerLiveCommand.Execute(null);
        StartLayerLiveCommand.Execute(null);
    }

    partial void OnSelectedMidiOutputChanged(OutputDevice? value)
    {
        if (!IsLayerLive)
            return;
        StopLayerLiveCommand.Execute(null);
        StartLayerLiveCommand.Execute(null);
    }

    partial void OnCurrentLayerChanged(XLayer? oldValue, XLayer? newValue)
    {
        if (oldValue != null)
        {
            LayerHistory.Push(oldValue);
            
            //Keep only the newest 10 layers in history
            if (LayerHistory.Count > 10)
                LayerHistory = new ConcurrentStack<XLayer>(LayerHistory.Take(10));
            
            oldValue.DisableLink();
        }

        //SelectedMidiOutput?.TurnAllNotesOff();
        ClearLEDs();

        if (newValue != null && _osc != null && SelectedMidiInput != null && SelectedMidiOutput != null)
        {
            // Enable link for the new layer
            newValue.InitLink(this, _osc, SelectedMidiInput, SelectedMidiOutput);
            newValue.UpdateValues();
        }
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
        //if (CurrentLayer != null)
        //{
        //    // Disable current layer
        //    CurrentLayer.DisableLink();
        //}

        Dispatcher.UIThread.Post(() => CurrentLayer = layer);

        //if (CurrentLayer != null && _osc != null && SelectedMidiInput != null && SelectedMidiOutput != null)
        //{
        //    // Enable new layer
        //    CurrentLayer.InitLink(this, _osc, SelectedMidiInput, SelectedMidiOutput);
        //    CurrentLayer.UpdateValues();
        //}
    }

    private void ClearLEDs()
    {
        foreach (var ring in XTouchMiniConstants.LEDRings)
            SelectedMidiOutput?.SendEvent(new ControlChangeEvent((SevenBitNumber)ring, (SevenBitNumber)0));
        foreach (var button in XTouchMiniConstants.BandButtonsRow1)
            SelectedMidiOutput?.SendEvent(new NoteOnEvent((SevenBitNumber)button, (SevenBitNumber)0));
        foreach (var button in XTouchMiniConstants.BandButtonsRow2)
            SelectedMidiOutput?.SendEvent(new NoteOnEvent((SevenBitNumber)button, (SevenBitNumber)0));
        SelectedMidiOutput?.SendEvent(
            new NoteOnEvent((SevenBitNumber)(int)XTouchMiniNotes.ButtonLayerUp, (SevenBitNumber)0));
        SelectedMidiOutput?.SendEvent(new NoteOnEvent((SevenBitNumber)(int)XTouchMiniNotes.ButtonLayerDown,
            (SevenBitNumber)0));
    }

    public void Dispose()
    {
        CurrentLayer?.DisableLink();
        SelectedMidiInput?.Dispose();
        SelectedMidiOutput?.Dispose();
        _osc?.Dispose();
    }
}