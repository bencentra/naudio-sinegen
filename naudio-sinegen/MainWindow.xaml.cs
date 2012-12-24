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

        //Default Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        //Event handler for clicking the button
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            //Grab the frequency value out of the text box
            float customFreq;
            if (freqBox.Text.Trim() == "")
            {
                customFreq = 440;
            }
            else
            {
                customFreq = float.Parse(freqBox.Text);
                if (customFreq < 20)
                {
                    customFreq = 20;
                }
                else if (customFreq > 20000)
                {
                    customFreq = 20000;
                }
            }
            freqBox.Text = customFreq.ToString();
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

        //Method for playing/stopping the sin wave
        private void StartStopSineWave(float freq)
        {
            //If the wave isn't playing, start it
            if (wave == null)
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
            //If the wave is playing, stop it
            else
            {
                //Stop the wave object and destroy it's data
                wave.Stop();
                wave.Dispose();
                wave = null;
            }
        }

    }
}
