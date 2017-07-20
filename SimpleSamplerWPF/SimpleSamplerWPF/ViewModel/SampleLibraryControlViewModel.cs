using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.ViewModel
{
    public class SampleLibraryControlViewModel : ViewModelBase
    {
        ISampleService sampleService;
        private ObservableCollection<Sample> samples;

        public RelayCommand AddSampleCommand { get; private set; }

        public SampleLibraryControlViewModel(ISampleService sampleService)
        {
            AddSampleCommand = new RelayCommand(AddSample);

            this.sampleService = sampleService;
            this.sampleService.GetSamples(
                (samp, error) =>
                {
                    Samples = samp;
                });
        }

        private void AddSample()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Supported Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                // add to sample service
                Sample newSample = new Sample(new CachedSound(openFileDialog.FileName), openFileDialog.SafeFileName);
                this.sampleService.AddSample(newSample);
            }
        }

        public ObservableCollection<Sample> Samples
        {
            get
            {
                return samples;
            }

            set
            {
                Set(ref samples, value);
            }
        }
    }
}
