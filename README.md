# ShapedAudioReverbZones
A Unity implementation for arbitrarily shaped Reverb Zones.
Default Unity reverb zones don't allow for box-shaped rooms, they also don't allow for overlapping reverb zones with priorities. This implementation seeks to fix that.

## Usage

There's a working example included in the project, though here's some really poor instructions anyway:

1. Put a AudioReverbConsumer on the audio listener, set the reverbLayer to where you'll place your reverb zones (IgnoreRaycast is a good one).
![image showing exposing of variables](https://cdn.discordapp.com/attachments/627827536717414410/730103394487894056/unknown.png)
2. Create an audio mixer and expose the SFX Reverb variables (you'll have to rename them too).
3. Create an empty gameobject in the scene, and add a AudioReverbArea to it.
4. Add a non-mesh collider to the empty gameobject and drag it into the Shape variable of the AudioReverbArea.
5. Set the AudioReverbArea preset as desired.
6. Ensure emitted audio is targetting the mixer with the SFX Reverb filter on it.
