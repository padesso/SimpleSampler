using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Design.Audio
{
    public class DesignSampleService : ISampleService
    {
        public void GetSamples(Action<ObservableCollection<Sample>, Exception> callback)
        {
            //Mock data
            ObservableCollection<Sample> samples = new ObservableCollection<Sample>();

            samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_AcouKick-07.wav"), "Test Kick"));
            samples.Add(new Sample(new CachedSound(@"TestAudio\CYCdh_K1close_Kick-01.wav"), "Test Close Kick"));

            callback(samples, null);
        }

        public void AddSample(Sample sound)
        {
            throw new NotImplementedException();
        }

        public void RemoveSample(Sample sound)
        {
            throw new NotImplementedException();
        }
    }
}
