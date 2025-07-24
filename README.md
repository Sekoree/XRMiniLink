The few functions I currently need from it work for me.<br>
The event handling code and how various data types are treated is very very ugly, if I get a better idea I might change it.

The current UI based editor is insanely complicated and many things probably dont work cause I didnt need them yet. I used this at HyperJapan London 2025 (for the VOCAFEST Virtual Theatre stage), worked well enough but I mostly just used 2-3 layers where I put my most used channels on (a few inputs, FX and outputs)

I couldnt find an OSC client library (that I got to work, probably skill issue) so I made [this](XRMiniLink/SimpleOSC.cs) very terrible thing with some help of [osc-core](https://github.com/space928/osc-core), very cool project so I didnt have to deal with the various OSC datatypes myself.<br>
For the MIDI stuff I use [drywetmidi](https://github.com/melanchall/drywetmidi) there's probably other MIDI libs out there, this worked for me.
