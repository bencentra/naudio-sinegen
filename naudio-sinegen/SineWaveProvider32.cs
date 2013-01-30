using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace naudio_sinegen
{
    /*
     * Sources:
     * - http://mark-dot-net.blogspot.com/2009/10/playback-of-sine-wave-in-naudio.html
     * - http://msdn.microsoft.com/en-us/magazine/ee309883.aspx
     */

    
    public class SineWaveProvider32 : WaveProvider32
    {
        //Initial frequency 
        protected const float INITIAL_FREQUENCY = 440f;

        //Variable to track the sample number
        protected float phaseAngle = 0;

        //Getters and setters for wave frequency and amplitude
        public float Frequency { get; set; }
        public float Amplitude { get; set; }

        //Default constructor
        public SineWaveProvider32()
        {
            Frequency = INITIAL_FREQUENCY;
            Amplitude = 0.25f;
        }

        //Parameterized constructor (for custom freq)
        public SineWaveProvider32(float freq)
        {
            Frequency = freq;
            Amplitude = 0.25f;
        }

        //Read in some data of the wave
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            Console.WriteLine("Old");
            //Get the sample rate
            int sampleRate = WaveFormat.SampleRate;
            //Loop through every sample
            for (int n = 0; n < sampleCount; n++)
            {
                //Determine the value of the current sample
                buffer[n + offset] = (float)(Amplitude * Math.Sin(phaseAngle));
                phaseAngle += (float) (2 * Math.PI * Frequency / sampleRate);
                if (phaseAngle > Math.PI * 2)
                    phaseAngle -= (float) (Math.PI * 2);
                //buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                //sample++;
                //if (sample >= sampleRate)
                //    sample = 0;
            }
            return sampleCount;
        }

    }

}
