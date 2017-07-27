using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using NAudio.Midi;
using SimpleSamplerWPF.Controls;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.MIDI;
using System;
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


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMidiDeviceService midiDeviceService)
        {
            samplerEngine = new MidiEngine();

            trackControls = new ObservableCollection<TrackControl>();
            AddTrackCommand = new RelayCommand(AddTrack);
            
            this.midiDeviceService = midiDeviceService;
            this.midiDeviceService.GetDeviceNames(
                (names, error) =>
                {
                    MidiDevices = names;
                });

            Messenger.Default.Register<TrackControl>(this, DeleteTrack);
        }

        /// <summary>
        /// Adds a new track control to the collection and thus, the UI.
        /// </summary>
        public void AddTrack()
        {
            TrackControl tc = new TrackControl();
            trackControls.Add(tc);
        }

        /// <summary>
        /// Removes a track track from the collection and thus, the UI.
        /// </summary>
        /// <param name="track"></param>
        public void DeleteTrack(TrackControl track)
        {
            trackControls.Remove(track);
        }

        /// <summary>
        /// Collection of MidiDevices bound to dropdown in UI.
        /// </summary>
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

        /// <summary>
        /// The selected Midi Device.
        /// </summary>
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

        /// <summary>
        /// Collection of track controls bound to ScrollViewer.
        /// </summary>
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

        /// <summary>
        /// Current build version. Bound to label in UI.
        /// </summary>
        public string Version
        {
            get { return "v " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString(); }
        }

        /// <summary>
        /// Called when shutting down.  
        /// </summary>
        public override void Cleanup()
        {
            // Get rid of the playback engine instance
            AudioPlaybackEngine.Instance.Dispose();

            base.Cleanup();
        }
    }
}