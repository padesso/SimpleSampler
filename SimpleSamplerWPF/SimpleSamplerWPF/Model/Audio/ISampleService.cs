﻿using SimpleSamplerWPF.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.Audio
{
    public interface ISampleService
    {
        void GetSamples(Action<ObservableCollection<Sample>, Exception> callback);
        void AddSample(Sample sound, Action<bool, Exception> callback);
        void RemoveSample(Sample sound, Action<bool, Exception> callback);
    }
}
