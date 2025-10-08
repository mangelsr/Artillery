# Artillery Notes

## Particle System

It's recommend to explore Unity's particle system with a package called `Unity Particle Pack`
The package it's now compatible with `URP`

We can also use `VFX graph` to create this kind of effects, this is much more complex but have a considerable better performance, this is work is mainly done by the technical artists

The three main properties:

- Emission
- Shape
- Renderer

## Material

We can find interesting textures for our FX on `opengameart.org`
It's important to consider that if the texture have a solid background color like black or white we are limited to just use them in `additive color mode`. If the texture have no BG it's better, we can use it in multiple modes

## Audio

Important to note the different formats that Unity support, the sound effects should have a format that differ from the Game Score

Spatial Audio is supported by Unity

The main classes are:

- Audio Listener
- Audio Source
  
  - Priority
  - Volume
  - Pitch

- Audio Mixer
  - Filters in Engine (May cause little lag on sound)

- Reverb zone (Echo zones)

Supported formats:

- AIFF
- WAV
- MP3
- OGG

Native Audio Plugin SDK for handling Audio in the most efficient way

### Audacity

Open Source tool to edit Audio tracks

Pro Tools is the industry standard software, but Audacity is a good option

## New Input System

Input System vs Input Manager

Input Manger => Pool key mapped keys, hard to remap controls

Input System => Uses an event system, very easy to remap controls and support multiple input types

Input Actions Set => Entries Map

Input Debugger
Window > Analysis > Input Debugger

Generic Map vs Specific Map
