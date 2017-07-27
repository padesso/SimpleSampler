using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System.Collections.ObjectModel;
using static SimpleSamplerWPF.ViewModel.TrackControlViewModel;

namespace SimpleSamplerWPF.ViewModel
{
    /// <summary>
    /// View model for sample library.
    /// </summary>
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

            Messenger.Default.Register<LearnModes>(this, LearnMode);

            this.sampleService = sampleService;
            this.sampleService.GetSamples(
                (samp, error) =>
                {
                    Samples = samp;
                });
        }

        /// <summary>
        /// Add a sample to the collection for binding.
        /// </summary>
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

        /// <summary>
        /// Informs the sample service to remove a sample.
        /// </summary>
        private void DeleteSample()
        {
            //safety check
            if(selectedSample != null)
                this.sampleService.RemoveSample(selectedSample);
        }

        /// <summary>
        /// Controls enablement of delete button.  Only allow a click if a sample is selected.
        /// </summary>
        /// <returns></returns>
        public bool CanDelete()
        {
            if (selectedSample != null)
                return true;

            return false;
        }

        /// <summary>
        /// The samples to be bound in the UI.
        /// </summary>
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

        /// <summary>
        /// The sample selected in the UI.
        /// </summary>
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

        /// <summary>
        /// If a learn mode is assigned from the track control, we deselect the sample.
        /// </summary>
        /// <param name="mode"></param>
        private void LearnMode(LearnModes mode)
        {
            SelectedSample = null; 
        }
    }
}
