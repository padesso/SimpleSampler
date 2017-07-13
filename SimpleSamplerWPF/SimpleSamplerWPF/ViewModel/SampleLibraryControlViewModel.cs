using GalaSoft.MvvmLight;
using SimpleSamplerWPF.Logic;
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
        private ObservableCollection<Sample> samples;

        public SampleLibraryControlViewModel()
        {
            Samples = new ObservableCollection<Sample>();

            //TEST:
            Samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "test name 1"));
            Samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "test name 2"));
            Samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "test name 3"));
            Samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "test name 4"));
            Samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "test name 5"));
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
