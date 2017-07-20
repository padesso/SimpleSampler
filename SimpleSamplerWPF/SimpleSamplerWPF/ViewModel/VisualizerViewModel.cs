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

// Visualizer converted to WPF and MVVM based on:
// http://www.giawa.com/tutorials/


namespace SimpleSamplerWPF.ViewModel
{
    public class VisualizerViewModel : ViewModelBase
    {
        private CachedSound sample;

        public RelayCommand PlaySampleCommand { get; private set; }

        public VisualizerViewModel()
        {           
            PlaySampleCommand = new RelayCommand(PlaySample, IsSampleLoaded);

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
    }
}
