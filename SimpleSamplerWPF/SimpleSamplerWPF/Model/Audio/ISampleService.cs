using SimpleSamplerWPF.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.Audio
{
    interface ISampleService
    {
        void GetSamples(Action<ObservableCollection<CachedSound>, Exception> callback);
        void AddSample(CachedSound sound, Action<bool, Exception> callback);
        void RemoveSample(CachedSound sound, Action<bool, Exception> callback);
    }
}
