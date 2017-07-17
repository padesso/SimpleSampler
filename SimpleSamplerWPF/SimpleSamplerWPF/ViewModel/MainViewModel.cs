using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using NAudio.Midi;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SimpleSamplerWPF.Controls;
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
        private MidiEngine samplerEngine;

        private readonly IMidiDeviceService midiDeviceService;
        private ObservableCollection<string> midiDevices;

        private ObservableCollection<TrackControl> trackControls;

        private int selectedMidiDeviceIndex = -1;

        public RelayCommand AddTrackCommand { get; private set; }
        public RelayCommand DeleteTrackCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMidiDeviceService midiDeviceService)
        {
            samplerEngine = new MidiEngine();

            trackControls = new ObservableCollection<TrackControl>();
            AddTrackCommand = new RelayCommand(AddTrack);
            DeleteTrackCommand = new RelayCommand(DeleteTrack);

            this.midiDeviceService = midiDeviceService;
            this.midiDeviceService.GetDeviceNames(
                (names, error) =>
                {
                    MidiDevices = names;
                });

            //TODO: pass selected index of device dropdown
            //this.midiDeviceService.GetDevice(0,
            //    (device, error) =>
            //    {
            //        //TODO: set a value and bind to UI
            //        var selectedDevice = device;
            //    });
        }

        public void AddTrack()
        {
            TrackControl tc = new TrackControl();
            trackControls.Add(tc);
        }

        public void DeleteTrack()
        {
            //TODO: Find a good way to remove the track
            //trackControls.Remove();
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

        public int SelectedMidiDeviceIndex
        {
            get
            {
                return selectedMidiDeviceIndex;
            }

            set
            {
                Set(ref selectedMidiDeviceIndex, value);

                samplerEngine.MidiIn = new MidiIn(SelectedMidiDeviceIndex);
            }
        }

        public ObservableCollection<TrackControl> TrackControls
        {
            get
            {
                return trackControls;
            }

            set
            {
                Set(ref trackControls, value);
            }
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}