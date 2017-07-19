using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSamplerWPF.Logic;

namespace SimpleSamplerWPF.Model.Audio
{
    public class SampleService : ISampleService
    {
        public void AddSample(Sample sound, Action<bool, Exception> callback)
        {
            throw new NotImplementedException();
        }

        public void GetSamples(Action<ObservableCollection<Sample>, Exception> callback)
        {
            //TODO: improve this
            ObservableCollection<Sample> samples = new ObservableCollection<Sample>();

            samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "Test Kick"));
            samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_K1close_Kick-01.wav"), "Test Close Kick"));

            callback(samples, null);
        }

        public void RemoveSample(Sample sound, Action<bool, Exception> callback)
        {
            throw new NotImplementedException();
        }
    }
}
