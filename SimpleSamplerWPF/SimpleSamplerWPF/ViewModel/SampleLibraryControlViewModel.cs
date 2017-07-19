using GalaSoft.MvvmLight;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
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
        ISampleService sampleService;
        private ObservableCollection<Sample> samples;
        
        public SampleLibraryControlViewModel(ISampleService sampleService)
        {
            this.sampleService = sampleService;
            this.sampleService.GetSamples(
                (samp, error) =>
                {
                    Samples = samp;
                });
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
