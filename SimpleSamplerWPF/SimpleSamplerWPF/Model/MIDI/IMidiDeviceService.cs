﻿using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.MIDI
{
    public interface IMidiDeviceService
    {
        void GetDeviceNames(Action<ObservableCollection<string>, Exception> callback);

        //TODO: how to pass a value to get the selected MidiIn???
        void GetDevice(int deviceID, Action<MidiInCapabilities, Exception> callback);
    }
}
