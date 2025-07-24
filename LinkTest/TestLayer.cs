using Melanchall.DryWetMidi.Core;
using OscCore.LowLevel;
using XRMiniLink.Data;

namespace LinkTest;

public static class TestLayer
{
    public static XLayer GetTestLayer()
    {
        return new XLayer()
        {
            Name = "Test Layer",
            IsDefault = true,
            Fader = new XFader()
            {
                MIDINote = 8, //channel 8?
                ValueMin = 0,
                ValueMax = 16256,
                OSCCommands = [
                    new XOSCCommand()
                    {
                        Command = "/headamp/000/gain",
                        OSCType = OscToken.Float,
                        Min = 0.0f,
                        Max = 1.0f,
                    }
                ]
            },
            Bands =
            [
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 16,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/01/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 48,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/01/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    TopButton = new XButton()
                    {
                        MIDINote = 89,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/-stat/solosw/01",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 0f,
                                Max = 1f, 
                            }
                        ]
                    },
                    TopButtonLED = new XButtonLED()
                    {
                        MIDINote = 89,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/-stat/solosw/01",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 87,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/01/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 87,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/01/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 17,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/02/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 49,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/02/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 88,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/02/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 88,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/02/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 18,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/03/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 50,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/03/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 91,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/03/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 91,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/03/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 19,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/04/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 51,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/04/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 92,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/04/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 92,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/04/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 20,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/05/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 52,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/05/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 86,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/05/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 86,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/05/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 21,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/06/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 53,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/06/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 93,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/06/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 93,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/06/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 22,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/07/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 54,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/07/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 94,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/07/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 94,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/07/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 23,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/08/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 55,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/08/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 95,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/08/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 95,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/08/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                }
            ],
            ButtonDown = new XButton()
            {
                MIDINote = 84,
                InternalCommands =
                [
                    new XInternalCommand()
                    {
                        Command = InternalCommand.SwitchLayer,
                        Parameters = "Test Layer2"
                    }
                ]
            },
            InitMIDICommands =
            [
                //new XMIDICommand()
                //{
                //    MidiNote = "89",
                //    MidiType = MidiEventType.NoteOn,
                //    Value = 127
                //}
            ],
            UnInitMIDICommands =
            [
                //new XMIDICommand()
                //{
                //    MidiNote = "89",
                //    MidiType = MidiEventType.NoteOn,
                //    Value = 0
                //}
            ]
        };
    }

    public static XLayer GetTestLayer2()
    {
        return new XLayer()
        {
            Name = "Test Layer2",
            IsDefault = true,
            Bands =
            [
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 16,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/09/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 48,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/09/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 87,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/09/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 87,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/09/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 17,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/10/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 49,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/10/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 88,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/10/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 88,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/10/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 18,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/11/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 50,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/11/mix/fader",
                            OSCType = OscToken.Float,
                            Value = "0.001",
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 91,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/11/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 91,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/11/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 19,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/12/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 51,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/12/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 92,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/12/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 92,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/12/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 20,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/13/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 52,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/13/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 86,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/13/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 86,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/13/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 21,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/14/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 53,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/14/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 93,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/14/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 93,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/14/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 22,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/15/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 54,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/15/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 94,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/15/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 94,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/15/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                },
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MIDINote = 23,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/16/mix/fader",
                                OSCType = OscToken.Float,
                                Value = "0.001",
                                Min = 0.0f,
                                Max = 1.0f,
                            }
                        ]
                    },
                    LEDRing = new XLEDRing()
                    {
                        MIDINote = 55,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/16/mix/fader",
                            OSCType = OscToken.Float,
                            Min = 0.0f,
                            Max = 1.0f,
                        }
                    },
                    BottomButton = new XButton() //mute toggle
                    {
                        MIDINote = 95,
                        OSCCommands =
                        [
                            new XOSCCommand()
                            {
                                Command = "/ch/16/mix/on",
                                OSCType = OscToken.Int,
                                IsToggle = true,
                                Min = 1f, //inverted because /on is mute=off
                                Max = 0f, 
                            }
                        ]
                    },
                    BottomButtonLED = new XButtonLED()
                    {
                        MIDINote = 95,
                        OSCCommand = new XOSCCommand()
                        {
                            Command = "/ch/16/mix/on",
                            OSCType = OscToken.Int,
                            IsToggle = true,
                            Min = 127.0f, //inverted because /on is mute=off 
                            Max = 0.0f,  
                        }
                    }
                }
            ],
            ButtonDown = new XButton()
            {
                MIDINote = 84,
                InternalCommands =
                [
                    new XInternalCommand()
                    {
                        Command = InternalCommand.SwitchLayer,
                        Parameters = "Test Layer"
                    }
                ]
            },
            InitMIDICommands =
            [
                new XMIDICommand()
                {
                    MIDINote = 89,
                    MidiType = MidiEventType.NoteOn,
                    Value = 127
                },
                new XMIDICommand()
                {
                    MIDINote = 90,
                    MidiType = MidiEventType.NoteOn,
                    Value = 127
                }
            ],
            UnInitMIDICommands =
            [
                new XMIDICommand()
                {
                    MIDINote = 89,
                    MidiType = MidiEventType.NoteOn,
                    Value = 0
                },
                new XMIDICommand()
                {
                    MIDINote = 90,
                    MidiType = MidiEventType.NoteOn,
                    Value = 0
                }
            ]
        };
    }
}