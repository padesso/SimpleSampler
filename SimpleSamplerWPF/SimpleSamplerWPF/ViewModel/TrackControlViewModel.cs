using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using SimpleSamplerWPF.Model.UI;

namespace SimpleSamplerWPF.ViewModel
{
    public class TrackControlViewModel : ViewModelBase
    {
        private TrackItem track;

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
    }
}
