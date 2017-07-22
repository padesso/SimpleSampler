using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NAudio.Wave;
using SimpleSamplerWPF.Helpers;
using SimpleSamplerWPF.Helpers.Events;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

// Visualizer converted to WPF and MVVM based on:
// http://www.giawa.com/tutorials/


namespace SimpleSamplerWPF.ViewModel
{
    public class VisualizerViewModel : ViewModelBase
    {
        private Sample selectedSample;

        public RelayCommand PlaySampleCommand { get; private set; }

        PointCollection polylinePoints;

        public VisualizerViewModel()
        {           
            PlaySampleCommand = new RelayCommand(PlaySample, IsSampleLoaded);

            Messenger.Default.Register<Sample>(this, m => SelectedSample = m);            
        }

        /// <summary>
        /// Play the selected sample.
        /// </summary>
        public void PlaySample()
        {
            //AudioPlaybackEngine.Instance.ReadWaveForm(Sample.CachedSound);

            if (selectedSample != null)
                AudioPlaybackEngine.Instance.PlaySound(SelectedSample.CachedSound);
        }

        private bool IsSampleLoaded()
        {
            if (selectedSample == null)
                return false;

            return true;
        }

        internal Sample SelectedSample
        {
            get
            {
                return selectedSample;
            }

            set
            {
                Set("SelectedSample", ref selectedSample, value);

                PlaySampleCommand.RaiseCanExecuteChanged(); //Notify the UI for play button state

                float[] data = selectedSample.CachedSound.AudioData;

                //TODO: move to center of visualizer, scale vertically and horizontally.
                polylinePoints = new PointCollection();
                for (int sampleIndex = 0; sampleIndex < data.Length; sampleIndex+=2)
                {
                    polylinePoints.Add(new Point(sampleIndex, (data[sampleIndex] * 250) + 50));
                }

                RaisePropertyChanged("PolylinePoints");
            }
        }

        public PointCollection PolylinePoints
        {
            get
            {
                return polylinePoints;
            }

            set
            {
                Set("PolylinePoints", ref polylinePoints, value);
            }
        }
    }
}
