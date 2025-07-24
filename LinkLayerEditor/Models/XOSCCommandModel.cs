using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using LinkLayerEditor.ViewModels;
using OscCore.LowLevel;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XOSCCommandModel : ViewModelBase
{
    [ObservableProperty]
    private string _command;
    
    [ObservableProperty]
    private OscToken _oscType;
    
    [ObservableProperty]
    private ObservableCollection<OscToken> _availableOscTypes = new(Enum.GetValues<OscToken>());
    
    [ObservableProperty]
    private bool _isToggle;
    
    [ObservableProperty]
    private bool _hasValue;
    
    [ObservableProperty]
    private string? _value;
    
    [ObservableProperty]
    private bool _hasMinMax;
    
    [ObservableProperty]
    private float? _min;
    
    [ObservableProperty]
    private float? _max;
    
    public XOSCCommand ToDataLayer()
    {
        return new XOSCCommand
        {
            Command = Command,
            OSCType = OscType,
            IsToggle = IsToggle,
            Value = HasValue ? Value : null,
            Min = HasMinMax ? Min : null,
            Max = HasMinMax ? Max : null
        };
    }
    
    public static XOSCCommandModel FromDataLayer(XOSCCommand command)
    {
        return new XOSCCommandModel
        {
            Command = command.Command,
            OscType = command.OSCType,
            IsToggle = command.IsToggle,
            HasValue = !string.IsNullOrWhiteSpace(command.Value),
            Value = command.Value,
            HasMinMax = command.Min.HasValue || command.Max.HasValue,
            Min = command.Min,
            Max = command.Max
        };
    }
}