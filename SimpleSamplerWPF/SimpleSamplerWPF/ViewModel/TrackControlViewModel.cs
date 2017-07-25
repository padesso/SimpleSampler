using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using SimpleSamplerWPF.Model.UI;
using System.Windows.Media;
using GalaSoft.MvvmLight.Command;
using NAudio.Midi;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System.Collections.ObjectModel;
using SimpleSamplerWPF.Controls;

namespace SimpleSamplerWPF.ViewModel
{
    public sealed class TrackControlViewModel : ViewModelBase
    {
        private bool isMaster;
        private TrackItem track;
        private LearnModes learnMode;
        private Color borderColor = Colors.Red;
        private NoteOnEvent noteOnMessage;

        public RelayCommand ToggleMidiLearnModeCommand { get; private set; }
        public RelayCommand ToggleSampleLearnModeCommand { get; private set; }
        public RelayCommand<TrackControl> DeleteTrackCommand { get; private set; }

        ISampleService sampleService;
        
        public enum LearnModes
        {
            None,
            MidiLearnMode,
            SampleLearnMode
        };

        public TrackControlViewModel(ISampleService sampleService, bool isMaster)
        {
            this.sampleService = sampleService;

            IsMaster = isMaster;

            ToggleMidiLearnModeCommand = new RelayCommand(ToggleMidiLearnMode);
            ToggleSampleLearnModeCommand = new RelayCommand(ToggleSampleLearnMode);
            DeleteTrackCommand = new RelayCommand<TrackControl>(DeleteTrack);

            track = new TrackItem();

            if (isMaster)
            {
                Name = "Master";
            }
            else
            {
                Messenger.Default.Register<NoteOnEvent>(this, note => NoteOnMessage = note);
                Messenger.Default.Register<Sample>(this, sample => SampleMessage = sample);
                Messenger.Default.Register<NotificationMessage>(this, NotificationMessageReceived);
            }
        }

        private void NotificationMessageReceived(NotificationMessage notification)
        {
            // If this is not the track control that sent the message, then set learn mode to None
            if (notification.Sender == this)
                return;

            LearnMode = LearnModes.None;
        }

        private void ToggleMidiLearnMode()
        {
            if (LearnMode == LearnModes.MidiLearnMode)
            {
                LearnMode = LearnModes.None;
            }
            else
            {
                LearnMode = LearnModes.MidiLearnMode;
                //Broadcast learn mode to deslect sample in library control so selection is captured
                Messenger.Default.Send<LearnModes>(LearnMode);

                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, LearnMode.ToString()));
            }
        }

        private void ToggleSampleLearnMode()
        {
            if (LearnMode == LearnModes.SampleLearnMode)
            {
                LearnMode = LearnModes.None;
            }
            else
            {
                LearnMode = LearnModes.SampleLearnMode;
                //Broadcast learn mode to deslect sample in library control so selection is captured
                Messenger.Default.Send<LearnModes>(LearnMode);

                Messenger.Default.Send<NotificationMessage>(new NotificationMessage(this, LearnMode.ToString()));
            }
        }

        private void DeleteTrack(TrackControl track)
        {
            Messenger.Default.Send<TrackControl>(track);
        }

        public float Volume
        {
            get
            {
                if(IsMaster)
                {
                    return AudioPlaybackEngine.Instance.MasterVolume;
                }
                
                return track.Volume;
            }

            set
            {
                if(IsMaster)
                {
                    AudioPlaybackEngine.Instance.MasterVolume = value;
                    RaisePropertyChanged("Volume");
                }
                else
                {
                    float previousVolume = track.Volume;
                    track.Volume = value;
                    RaisePropertyChanged("Volume", previousVolume, value, true);
                }                
            }
        }

        public float Pan
        {
            get
            {
                return track.Pan;
            }

            set
            {
                float previousPan = track.Pan;
                track.Pan = value;
                RaisePropertyChanged("Pan", previousPan, value, true);
            }
        }

        public string Name
        {
            get
            {
                return track.Name;
            }

            set
            {
                string previousName = track.Name;
                track.Name = value;
                RaisePropertyChanged("Name", previousName, value, true);
            }
        }

        public LearnModes LearnMode
        {
            get
            {
                return learnMode;
            }

            set
            {  
                Set(ref learnMode, value);
            }
        }

        public bool IsMaster
        {
            get
            {
                return isMaster;
            }

            set
            {
                Set(ref isMaster, value);
            }
        }

        public NoteOnEvent NoteOnMessage
        {
            get
            {
                return noteOnMessage;
            }

            set
            {             
                Set(ref noteOnMessage, value);

                if(LearnMode == LearnModes.MidiLearnMode)
                {
                    track.NoteNumber = noteOnMessage.NoteNumber;
                    LearnMode = LearnModes.None;

                    RaisePropertyChanged("NoteNumber");
                }                
                else if (noteOnMessage.NoteNumber == track.NoteNumber)  // Check if note is good for this track and play sample if so
                {
                    if(track.Sample != null)
                    {
                        AudioPlaybackEngine.Instance.PlaySound(track.Sample.CachedSound, Volume, false);
                    }
                }
            }
        }

        public Sample SampleMessage
        {
            get
            {
                return track.Sample;
            }

            set
            {
                if (LearnMode == LearnModes.SampleLearnMode)
                {
                    if (value != null)
                    {
                        Sample previousSample = track.Sample;
                        track.Sample = value;
                        Name = track.Sample.Name;

                        RaisePropertyChanged("SampleMessage", previousSample, value, true);

                        LearnMode = LearnModes.None;
                    }
                }
            }
        }

        public string NoteNumber
        {
            get
            {
                return track.NoteNumber.ToString();
            }
        }
    }
}
