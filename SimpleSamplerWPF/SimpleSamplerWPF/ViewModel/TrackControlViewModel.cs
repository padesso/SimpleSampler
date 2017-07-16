using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using SimpleSamplerWPF.Model.UI;
using System.Windows.Media;

namespace SimpleSamplerWPF.ViewModel
{
    public class TrackControlViewModel : ViewModelBase
    {
        private TrackItem track;
        private bool learnMode;
        private Color borderColor = Colors.Red;

        public TrackControlViewModel()
        {
            //TODO: get the tracks from a service or something
            track = new TrackItem();
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
    }
}
