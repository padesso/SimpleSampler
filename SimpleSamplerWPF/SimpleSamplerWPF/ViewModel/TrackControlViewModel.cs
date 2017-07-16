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

namespace SimpleSamplerWPF.ViewModel
{
    public class TrackControlViewModel : ViewModelBase
    {
        private bool isMaster;
        private TrackItem track;
        private bool learnMode;
        private Color borderColor = Colors.Red;

        public RelayCommand ToggleLearnModeCommand { get; private set; }

        public TrackControlViewModel()
        {
            IsMaster = false;

            ToggleLearnModeCommand = new RelayCommand(ToggleLearnMode);

            //TODO: get the tracks from a service or something
            track = new TrackItem();
        }

        private void ToggleLearnMode()
        {
            LearnMode = !LearnMode;
        }

        public float Volume
        {
            get
            {
                return track.Volume;
            }

            set
            {
                float previousVolume = track.Volume;
                track.Volume = value;
                RaisePropertyChanged("Volume", previousVolume, value, true);
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

        public bool LearnMode
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
    }
}
