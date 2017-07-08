using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model;
using SimpleSamplerWPF.Model.MIDI;
using System.Collections.ObjectModel;

namespace SimpleSamplerWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IWavePlayer waveOut;
        private MixingSampleProvider mixer;

        private readonly IMidiDeviceService midiDeviceService;
        private ObservableCollection<string> midiDevices;

        public RelayCommand AddTrackCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMidiDeviceService midiDeviceService)
        {
            AddTrackCommand = new RelayCommand(TestSound);

            this.midiDeviceService = midiDeviceService;
            this.midiDeviceService.GetDeviceNames(
                (names, error) =>
                {
                    MidiDevices = names;
                });

            //TODO: pass selected index of device dropdown
            this.midiDeviceService.GetDevice(0,
                (device, error) =>
                {
                    //TODO: set a value and bind to UI
                    var selectedDevice = device;
                });
        }

        public void TestSound()
        {
            var boom = new CachedSound(@"TestAudio\CYCdh_K1close_Kick-01.wav");
            AudioPlaybackEngine.Instance.PlaySound(boom);
        }

        private void OpenFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "All Supported Files (*.wav;*.mp3)|*.wav;*.mp3|All Files (*.*)|*.*";
            bool? result = openFileDialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                //this.selectedFile = openFileDialog.FileName;
                //audioPlayback.Load(this.selectedFile);

                //TODO: copy file to project directory, add to list view and cache
            }
        }

        public ObservableCollection<string> MidiDevices
        {
            get
            {
                return midiDevices;
            }

            set
            {
                Set(ref midiDevices, value);
            }
        }


        

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}