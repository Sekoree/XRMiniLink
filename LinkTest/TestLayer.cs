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
            Bands =
            [
                new XBand()
                {
                    Encoder = new XEncoder()
                    {
                        MidiNote = "16",
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
                        MidiNote = "48",
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
                        MidiNote = "89",
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
                        MidiNote = "89",
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
                        MidiNote = "87",
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
                        MidiNote = "87",
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
                        MidiNote = "17",
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
                        MidiNote = "49",
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
                        MidiNote = "88",
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
                        MidiNote = "88",
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
                        MidiNote = "18",
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
                        MidiNote = "50",
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
                        MidiNote = "91",
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
                        MidiNote = "91",
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
                        MidiNote = "19",
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
                        MidiNote = "51",
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
                        MidiNote = "92",
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
                        MidiNote = "92",
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
                        MidiNote = "20",
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
                        MidiNote = "52",
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
                        MidiNote = "86",
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
                        MidiNote = "86",
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
                        MidiNote = "21",
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
                        MidiNote = "53",
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
                        MidiNote = "93",
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
                        MidiNote = "93",
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
                        MidiNote = "22",
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
                        MidiNote = "54",
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
                        MidiNote = "94",
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
                        MidiNote = "94",
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
                        MidiNote = "23",
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
                        MidiNote = "55",
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
                        MidiNote = "95",
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
                        MidiNote = "95",
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
                MidiNote = "84",
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
            UnInitializeMIDICommands =
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
                        MidiNote = "16",
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
                        MidiNote = "48",
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
                        MidiNote = "87",
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
                        MidiNote = "87",
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
                        MidiNote = "17",
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
                        MidiNote = "49",
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
                        MidiNote = "88",
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
                        MidiNote = "88",
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
                        MidiNote = "18",
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
                        MidiNote = "50",
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
                        MidiNote = "91",
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
                        MidiNote = "91",
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
                        MidiNote = "19",
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
                        MidiNote = "51",
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
                        MidiNote = "92",
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
                        MidiNote = "92",
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
                        MidiNote = "20",
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
                        MidiNote = "52",
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
                        MidiNote = "86",
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
                        MidiNote = "86",
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
                        MidiNote = "21",
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
                        MidiNote = "53",
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
                        MidiNote = "93",
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
                        MidiNote = "93",
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
                        MidiNote = "22",
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
                        MidiNote = "54",
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
                        MidiNote = "94",
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
                        MidiNote = "94",
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
                        MidiNote = "23",
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
                        MidiNote = "55",
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
                        MidiNote = "95",
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
                        MidiNote = "95",
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
                MidiNote = "84",
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
                    MidiNote = "89",
                    MidiType = MidiEventType.NoteOn,
                    Value = 127
                },
                new XMIDICommand()
                {
                    MidiNote = "90",
                    MidiType = MidiEventType.NoteOn,
                    Value = 127
                }
            ],
            UnInitializeMIDICommands =
            [
                new XMIDICommand()
                {
                    MidiNote = "89",
                    MidiType = MidiEventType.NoteOn,
                    Value = 0
                },
                new XMIDICommand()
                {
                    MidiNote = "90",
                    MidiType = MidiEventType.NoteOn,
                    Value = 0
                }
            ]
        };
    }
}