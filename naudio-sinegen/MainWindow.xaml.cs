using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;

namespace naudio_sinegen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Variable to output the sin wave
        private WaveOut wave;

        //Min and Max allowable frequencies
        private const float MAX_FREQ = 5000;
        private const float MIN_FREQ = 50;
        private const float START_FREQ = 440;

        //Default Constructor
        public MainWindow()
        {
            InitializeComponent();
            freqBox.Text = START_FREQ.ToString();
            freqSlider.Value = (START_FREQ / MAX_FREQ) * 10;
        }

        //Event handler for clicking the play button
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the frequency of the wave from the text box
            float customFreq = GetTextFrequency();
            //Play or stop the sine wave
            StartStopSineWave(customFreq);
            //Set the text of the button appropirately
            if (wave != null)
            {
                playButton.Content = "Stop";
            }
            else
            {
                playButton.Content = "Play";
            }
        }

        //Method for toggling the sine wave
        private void StartStopSineWave(float freq)
        {
            //If the wave isn't playing, start it
            if (wave == null)
            {
                StartSineWave(freq);
            }
            //If the wave is playing, stop it
            else
            {
                //Stop the wave object and destroy it's data
                StopSineWave();
            }
        }

        //Event handler for clicking the update button
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            //Get the frequency of the wave from the text box
            float customFreq = GetTextFrequency();
            //Update the sine wave
            UpdateSineWave(customFreq);
        }

        //Update the frequency of the sine wave without stopping it (sorta)
        private void UpdateSineWave(float freq)
        {
            StopSineWave();
            StartSineWave(freq);
        }

        //Start playing the sine wave
        private void StartSineWave(float freq)
        {
            if (freq == -1)
            {
                freq = 440;
            }
            //Create a new sine wave provider
            var sineWaveProvider = new SineWaveProvider32();
            //Set the sample rate, number of channels, frequency and amplitude
            sineWaveProvider.SetWaveFormat(44100, 1); //16kHz, mono
            sineWaveProvider.Frequency = freq;
            sineWaveProvider.Amplitude = 0.25f;
            //Instantiate the wave output object
            wave = new WaveOut();
            //Initialize and play the sine wave
            wave.Init(sineWaveProvider);
            wave.Play();
        }

        //Stop playing the sine wave
        private void StopSineWave()
        {
            wave.Stop();
            wave.Dispose();
            wave = null;
        }

        //Grab the frequency value out of the text box
        private float GetTextFrequency()
        {
            float customFreq;
            if (freqBox.Text.Trim() == "")
            {
                customFreq = 440;
            }
            else
            {
                customFreq = float.Parse(freqBox.Text);
                if (customFreq < MIN_FREQ)
                {
                    customFreq = MIN_FREQ;
                }
                else if (customFreq > MAX_FREQ)
                {
                    customFreq = MAX_FREQ;
                }
            }
            freqBox.Text = customFreq.ToString();
            freqSlider.Value = (customFreq / MAX_FREQ) * 10;
            return customFreq;
        }

        //Event handler for changing the frequency slider
        private void freqSlider_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Console.Out.WriteLine(freqSlider.Value);
            float freq = GetSliderFrequency();
            if (wave != null)
                UpdateSineWave(freq);
        }

        //Get the frequency based on the slider's position
        private float GetSliderFrequency()
        {
            float value = (float)freqSlider.Value;
            float customFreq = MAX_FREQ * (value / 10);
            if (customFreq < MIN_FREQ)
            {
                customFreq = MIN_FREQ;
            }
            else if (customFreq > MAX_FREQ)
            {
                customFreq = MAX_FREQ;
            }
            freqBox.Text = customFreq.ToString();
            freqSlider.Value = (customFreq / MAX_FREQ) * 10;
            return customFreq;
        }

    }
}
