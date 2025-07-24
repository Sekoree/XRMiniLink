using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LinkLayerEditor.ViewModels;
using XRMiniLink.Data;

namespace LinkLayerEditor.Models;

public partial class XInternalCommandModel : ViewModelBase
{
    [ObservableProperty] private InternalCommand _command = InternalCommand.SwitchLayer;

    [ObservableProperty]
    private ObservableCollection<InternalCommand> _availableCommands = new(Enum.GetValues<InternalCommand>());

    [ObservableProperty] private string? _parameters;
    
    public XInternalCommand ToDataLayer()
    {
        return new XInternalCommand
        {
            Command = Command,
            Parameters = Parameters
        };
    }

    public static XInternalCommandModel FromDataLayer(XInternalCommand command)
    {
        return new XInternalCommandModel()
        {
            Command = command.Command,
            Parameters = command.Parameters
        };
    }
}