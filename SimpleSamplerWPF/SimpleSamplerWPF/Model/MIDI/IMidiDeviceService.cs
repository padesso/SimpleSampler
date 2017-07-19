using NAudio.Midi;
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

        void GetDevice(int deviceID, Action<MidiInCapabilities, Exception> callback);
    }
}
