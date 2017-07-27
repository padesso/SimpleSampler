using SimpleSamplerWPF.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.Audio
{
    /// <summary>
    /// Service for controlling samples in the sample library.
    /// </summary>
    public interface ISampleService
    {
        void GetSamples(Action<ObservableCollection<Sample>, Exception> callback);
        void AddSample(Sample sound);
        void RemoveSample(Sample sound);
    }
}
