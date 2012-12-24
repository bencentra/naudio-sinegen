using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace naudio_sinegen
{
    public class SineWaveProvider32 : WaveProvider32
    {
        //Variable to track the 
        int sample;

        //Getters and setters for wave frequency and amplitude
        public float Frequency { get; set; }
        public float Amplitude { get; set; }

        //Default Constructor
        public SineWaveProvider32()
        {
            Frequency = 440;
            Amplitude = 0.25f;
        }

        //Constructor using a custom frequency
        public SineWaveProvider32(float freq)
        {
            Frequency = freq;
            Amplitude = 0.25f;
        }

        //Read in some data of the wave
        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            //Get the sample rate
            int sampleRate = WaveFormat.SampleRate;
            //Loop through every sample
            for (int n = 0; n < sampleCount; n++)
            {
                //Determine the value of the current sample
                buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * sample * Frequency) / sampleRate));
                sample++;
                if (sample >= sampleRate)
                {
                    sample = 0;
                }
            }
            return sampleCount;
        }
    }
}
