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
        WaveStream waveStream;
        private Sample selectedSample;

        public RelayCommand PlaySampleCommand { get; private set; }

        PointCollection polylinePoints;

        public VisualizerViewModel()
        {           
            PlaySampleCommand = new RelayCommand(PlaySample, IsSampleLoaded);

            Messenger.Default.Register<Sample>(this, m => SelectedSample = m);

            //TEST DATA
            polylinePoints = new PointCollection();
            polylinePoints.Add(new Point(10, 100));
            polylinePoints.Add(new Point(100, 200));
            polylinePoints.Add(new Point(200, 30));
            polylinePoints.Add(new Point(250, 200));
            polylinePoints.Add(new Point(200, 150));
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
