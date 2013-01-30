using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace naudio_sinegen
{
    public class SineWavePlayer
    {
        //Variables for outputting a sine wave
        private WaveOut _wave;
        private SineWaveProvider32 _sineWaveProvider;

        //Wave properties
        private float _frequency;
        private float _amplitude;

        //Min and Max allowable frequencies
        private const float MIN_FREQ = 200f;
        private const float MAX_FREQ = MIN_FREQ * 10;

        //Getter/setter for _frequency
        public float Frequency
        {
            get { return _frequency; }
            set { _frequency = ConstrainFrequency(value); }
        }

        //Getter/Setter for _amplitude
        public float Amplitude
        {
            get { return _amplitude; }
            set { _amplitude = ConstrainAmplitude(value); }
        }

        //Getters for min and max frequency
        public float MinFreq { get { return MIN_FREQ; } }
        public float MaxFreq { get { return MAX_FREQ; } }

        //Default Constructor
        public SineWavePlayer()
        {
            _frequency = MIN_FREQ;
            _amplitude = .25f;
        }

        //Parameterized Constructor
        public SineWavePlayer(float initFreq, float initAmp)
        {
            _frequency = initFreq;
            _amplitude = initAmp;
        }

        //Stop or start the wave
        public void StartStopWave()
        {
            if (_wave == null)
                Start();
            else
                Stop();
        }

        //Start playing the wave
        public void Start()
        {
            if (_wave == null)
            {
                //Create a new sine wave provider if not already instantiated
                if (_sineWaveProvider == null)
                    _sineWaveProvider = new PortamentoSineWaveProvider32();
                //Set the sample rate, number of channels, frequency and amplitude
                _sineWaveProvider.SetWaveFormat((int)(MAX_FREQ * 2) + (int)MIN_FREQ, 1); //sample rate (2x MAX_FREQ + MIN_FREQ for safety), # channels (only want 1)
                _sineWaveProvider.Frequency = _frequency;
                _sineWaveProvider.Amplitude = _amplitude;
                //Instantiate the wave output object
                _wave = new WaveOut();
                //Initialize and play the sine wave
                _wave.Init(_sineWaveProvider);
                _wave.Play();
            }
        }

        //Stop playing the wave
        public void Stop()
        {
            if (_wave != null)
            {
                _wave.Stop();
                _wave.Dispose();
                _wave = null;
            }
        }

        //Determine if the wave is currently playing or stopped (is or is not null)
        public bool IsPlaying()
        {
            if (_wave == null)
                return false;
            else
                return true;
        }

        //Update the frequency of the sine wave provider 
        public void UpdateFrequency(float freq)
        {
            if (_sineWaveProvider != null)
            {
                _frequency = freq;
                _sineWaveProvider.Frequency = _frequency;
            }
        }

        //Update the amplitude of the sine wave provider
        public void UpdateAmplitude(float amp)
        {
            if (_sineWaveProvider != null)
            {
                _amplitude = amp;
                _sineWaveProvider.Amplitude = _amplitude;
            }
        }

        //Keep the frequency in the appropriate range
        public static float ConstrainFrequency(float freq)
        {
            if (freq < MIN_FREQ)
                freq = MIN_FREQ;
            else if (freq > MAX_FREQ)
                freq = MAX_FREQ;
            return freq;
        }

        //Keep the amplitude in the appropriate range
        public static float ConstrainAmplitude(float amp)
        {
            if (amp < 0)
                amp = 0;
            if (amp > 1)
                amp = 1;
            return amp;
        }

    }
}
