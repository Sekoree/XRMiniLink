The few functions I currently need from it work for me.<br>
The event handling code and how various data types are treated is very very ugly, if I get a better idea I might change it.

At some point I'll probably make a proper UI for creating layers (lie), but currently I just code them, serialize and stitch together.

I couldnt find an OSC client library (that I got to work, probably skill issue) so my made [this](XRMiniLink/SimpleOSC.cs) very terrible thing with some help of [osc-core](https://github.com/space928/osc-core), very cool project so I didnt have to deal with the various OSC datatypes myself.<br>
For the MIDI stuff I use [drywetmidi](https://github.com/melanchall/drywetmidi) there's probably other MIDI libs out there, this worked for me.
