using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleSamplerWPF.Logic;

namespace SimpleSamplerWPF.Model.Audio
{
    /// <summary>
    /// Implementation for controlling samples in the sample library.
    /// </summary>
    public class SampleService : ISampleService
    {
        private ObservableCollection<Sample> samples = new ObservableCollection<Sample>();

        public void GetSamples(Action<ObservableCollection<Sample>, Exception> callback)
        {            
            callback(samples, null);
        }

        public void AddSample(Sample sound)
        {
            samples.Add(sound);
        }
        
        public void RemoveSample(Sample sound)
        {
            samples.Remove(sound);
        }
    }
}
