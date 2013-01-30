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

    public class PortamentoSineWaveProvider32 : SineWaveProvider32
    {
        //The last frequency played
        protected float lastFrequency;

        //Default constructor
        public PortamentoSineWaveProvider32()
            : base()
        {
            lastFrequency = Frequency;
        }

        //Parameterized constructor (for custom freq)
        public PortamentoSineWaveProvider32(float freq)
            : base(freq)
        {
            lastFrequency = Frequency;
        }

        public override int Read(float[] buffer, int offset, int sampleCount)
        {
            Console.WriteLine("New");
            //Get the sample rate
            int sampleRate = WaveFormat.SampleRate;
            //Loop through every sample
            for (int n = 0; n < sampleCount; n++)
            {
                //If the frequency changed, transition smoothly into the new frequency (portamento)
                float freq = Frequency;
                if (Frequency != lastFrequency)
                {
                    freq = ((sampleCount - n - 1) * lastFrequency + Frequency) / (sampleCount - n);
                    lastFrequency = freq;
                }
                //Determine the value of the current sample
                buffer[n + offset] = (float)(Amplitude * Math.Sin(phaseAngle));
                phaseAngle += (float)(2 * Math.PI * freq / sampleRate);
                if (phaseAngle > Math.PI * 2)
                    phaseAngle -= (float)(Math.PI * 2);
            }
            return sampleCount;
        }
    }
}
