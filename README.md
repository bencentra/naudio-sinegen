# naudio-sinegen #
@author Ben Centra (bencentra@csh.rit.edu)

A simple sine wave generator, made in C# using the [NAudio] (http://naudio.codeplex.com/ "NAudio") library. 

It serves as experimentation and groundwork for another project of mine, kinect-theremin.

## How-to ##

_Using the Text Box_

1) Type a desired frequency (float) into the text box (ex - 440).
2) Hit the "Play" button to start playing the wave. Hit it again to stop the wave.
3) To update the frequency, type a new frequency into the text box and hit the "Update" button.

_Using the Slider_

1) Move the slider around, and the frequency will update automatically!

## Version History ##

### v1.2 - 12/23/2012 ###

* Added MIN_FREQ, MAX_FREQ, and START_FREQ variables
* Added the slider, can update the frequency dynamically (though not smoothly)

### v1.1 - 12/23/2012 ###

* Refactored some code, yay efficiency!
* Can update the frequency using the "Update" button without stopping the sine wave

### v1.0 - 12/23/2012 ###
* Added to GitHub
* Accepts a frequency (from 20Hz - 20kHz) in a text box
* "Play" button for playing the desired frequency, same button for stopping.