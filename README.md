# naudio-sinegen #
@author Ben Centra (bencentra@csh.rit.edu)

A simple sine wave generator, made in C# using the [NAudio] (http://naudio.codeplex.com/ "NAudio") library. 

Also uses [WpfKinectHelper] (https://github.com/bencentra/WpfKinectHelper), a plug-and-play Kinect library of mine.

This project serves as experimentation and groundwork for another project of mine, kinect-theremin.

## How-to ##

_Using the Text Box_

1) Type a desired frequency (float) into the text box (ex - 440).
2) Hit the "Play" button to start playing the wave. Hit it again to stop the wave.
3) To update the frequency, type a new frequency into the text box and hit the "Update" button.

_Using the Slider_

1) After hitting play, move the slider around and the frequency will update automatically!

_Using a Kinect_

1) Plug a Kinect in to your computer and launch the application.
2) Stand in front of the Kinect and wait until your skeleton is being tracked.
3) After hitting play, use your left hand to control frequency and your right hand to control amplitude!

## Version History ##

### v1.3 - 1/10/2013 ###

* Added amplitude control.
* Added Kinect controls.
* Added PortamentoSineWaveProvider32.cs, for smooth transitions between frequencies.

### v1.2 - 12/23/2012 ###

* Added MIN_FREQ, MAX_FREQ, and START_FREQ variables.
* Added the slider, can update the frequency dynamically (though not smoothly).

### v1.1 - 12/23/2012 ###

* Refactored some code, yay efficiency!
* Can update the frequency using the "Update" button without stopping the sine wave.

### v1.0 - 12/23/2012 ###
* Added to GitHub
* Accepts a frequency (from 20Hz - 20kHz) in a text box.
* "Play" button for playing the desired frequency, same button for stopping.