using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using SimpleSamplerWPF.Helpers;
using SimpleSamplerWPF.Helpers.Events;
using SimpleSamplerWPF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.ViewModel
{
    public class VisualizerViewModel : ViewModelBase
    {
        private CachedSound sample;
        private IVisualization visualization;

        public RelayCommand PlaySampleCommand { get; private set; }

        public VisualizerViewModel()
        {
            visualization = new PolylineWaveFormVisualization();

            AudioPlaybackEngine.Instance.MaximumCalculated += audioGraph_MaximumCalculated;
            AudioPlaybackEngine.Instance.FftCalculated += audioGraph_FftCalculated;

            PlaySampleCommand = new RelayCommand(PlaySample, IsSampleLoaded);

            //TODO: hook into the FFT events to create the waveform

            //TEST ONLY!
            sample = new CachedSound(@"TestAudio\CYCdh_K1close_Kick-01.wav");
        }

        /// <summary>
        /// Play the selected sample.
        /// </summary>
        public void PlaySample()
        {
            AudioPlaybackEngine.Instance.ReadWaveForm(sample);

            if (sample != null)
                AudioPlaybackEngine.Instance.PlaySound(sample);
        }

        private bool IsSampleLoaded()
        {
            //TODO: do we need to look at other params?
            if (sample == null)
                return false;

            return true;
        }

        internal CachedSound Sample
        {
            get
            {
                return sample;
            }

            set
            {
                Set("Sample", ref sample, value);
            }
        }

        public object Visualization
        {
            get
            {
                return this.visualization.Content;
            }
        }

        void audioGraph_FftCalculated(object sender, FftEventArgs e)
        {
            if (this.visualization != null)
            {
                this.visualization.OnFftCalculated(e.Result);
            }
        }

        void audioGraph_MaximumCalculated(object sender, MaxSampleEventArgs e)
        {
            if (this.visualization != null)
            {
                this.visualization.OnMaxCalculated(e.MinSample, e.MaxSample);
            }
        }
    }
}
