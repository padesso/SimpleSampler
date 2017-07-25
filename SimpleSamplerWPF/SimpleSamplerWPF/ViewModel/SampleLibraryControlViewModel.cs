using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System.Collections.ObjectModel;

namespace SimpleSamplerWPF.ViewModel
{
    public class SampleLibraryControlViewModel : ViewModelBase
    {
        ISampleService sampleService;
        private ObservableCollection<Sample> samples;

        private Sample selectedSample;

        public RelayCommand AddSampleCommand { get; private set; }
        public RelayCommand DeleteSampleCommand { get; private set; }

        public SampleLibraryControlViewModel(ISampleService sampleService)
        {
            AddSampleCommand = new RelayCommand(AddSample);
            DeleteSampleCommand = new RelayCommand(DeleteSample, CanDelete);

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

        private void DeleteSample()
        {
            //safety check
            if(selectedSample != null)
                this.sampleService.RemoveSample(selectedSample);
        }

        public bool CanDelete()
        {
            if (selectedSample != null)
                return true;

            return false;
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

        //TODO: this doesn't fire when clicking on an already selected sample but we need 
        //it to so the learning stuff works as expected.
        public Sample SelectedSample
        {
            get
            {
                return selectedSample;
            }

            set
            {
                Set(ref selectedSample, value);
                DeleteSampleCommand.RaiseCanExecuteChanged(); //Notify the UI for delete button state

                // send message to the visualizer to display the waveform
                Messenger.Default.Send(selectedSample);
            }
        }
    }
}
