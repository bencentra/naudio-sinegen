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
using Microsoft.Kinect;

namespace naudio_sinegen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Wave Player
        private SineWavePlayer player = new SineWavePlayer();

        //Kinect Helper
        private KinectHelper helper;

        //Default Constructor
        public MainWindow()
        {
            InitializeComponent();
            //player = new SineWavePlayer();
            freqBox.Text = player.Frequency.ToString();
            freqSlider.Value = player.Frequency;
            volBox.Text = (player.Amplitude * 100).ToString();
            //Initialize the Kinect Sensor
            helper = new KinectHelper(true, false, true);
            skelImage.Source = helper.skeletonBitmap;
            rgbImage.Source = helper.colorBitmap;
            helper.ToggleSeatedMode(true);
            helper.SkeletonDataChanged += new KinectHelper.SkeletonDataChangedEvent(SkeletonDataChange);
        }

        //Event handler for clicking the play button
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            if (!player.IsPlaying())
            {
                player.Frequency = SineWavePlayer.ConstrainFrequency(float.Parse(freqBox.Text.Trim()));
                player.Amplitude = SineWavePlayer.ConstrainAmplitude(float.Parse(volBox.Text.Trim()) / 100);
                player.Start();
                playButton.Content = "Stop";
            }
            else
            {
                player.Stop();
                playButton.Content = "Play";
            }
        }

        //Event handler for clicking the update button
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            float customFreq = SineWavePlayer.ConstrainFrequency(float.Parse(freqBox.Text.Trim()));
            float customAmp = SineWavePlayer.ConstrainAmplitude(float.Parse(volBox.Text.Trim()) / 100);
            player.UpdateFrequency(customFreq);
            player.UpdateAmplitude(customAmp);
            freqBox.Text = customFreq.ToString();
            volBox.Text = (customAmp * 100).ToString();
        }

        //Event handler for changing the frequency slider
        private void freqSlider_Change(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            float customFreq = SineWavePlayer.ConstrainFrequency((float)freqSlider.Value);
            if (player.IsPlaying())
                player.UpdateFrequency((customFreq));
            freqBox.Text = customFreq.ToString();
        }

        //Event handler for changes in the Kinect's skeleton data
        private void SkeletonDataChange(object sender, SkeletonDataChangeEventArgs e)
        {
            //Get the coordinates of the right and left hands
            Skeleton skel = null;
            for (int i = 0; i < e.skeletons.Length; i++)
            {
                if (e.skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                {
                    skel = e.skeletons[i];
                    break;
                }
            }
            if (skel == null)
                return;
            Point rightHand = helper.SkeletonPointToScreen(skel.Joints[JointType.HandRight].Position);
            rightLabel.Content = "(" + rightHand.X + "," + rightHand.Y + ")";
            Point leftHand = helper.SkeletonPointToScreen(skel.Joints[JointType.HandLeft].Position);
            leftLabel.Content = "(" + leftHand.X + "," + leftHand.Y + ")";

            //Adjust the frequency based on the position of the left hand
            float ratio = player.MaxFreq / player.MinFreq;
            float customFreq = (float)(player.MaxFreq - (ratio * leftHand.Y / skelImage.Height) * player.MinFreq);
            customFreq = SineWavePlayer.ConstrainFrequency(customFreq);
            if (player.IsPlaying())
            {
                player.UpdateFrequency(customFreq);
                freqSlider.Value = customFreq;
                freqBox.Text = customFreq.ToString();
            }

            //Adjust the amplitude based on the position of the right hand
            float customAmp = (float)(1 - (rightHand.Y / skelImage.Height));
            customAmp = SineWavePlayer.ConstrainAmplitude(customAmp);
            if (player.IsPlaying())
            {
                player.UpdateAmplitude(customAmp);
                volBox.Text = (customAmp * 100).ToString();
            }
        }

    }
}
