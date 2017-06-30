using SimpleSamplerWPF.Model.MIDI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Midi;
using System.Collections.ObjectModel;

namespace SimpleSamplerWPF.Design.MIDI
{
    public class DesignMidiDeviceService : IMidiDeviceService
    {
        public void GetDevice(int deviceID, Action<MidiInCapabilities, Exception> callback)
        {
            //TODO:
        }

        public void GetDeviceNames(Action<ObservableCollection<string>, Exception> callback)
        {
            ObservableCollection<string> names = new ObservableCollection<string>();

            for (int device = 0; device < 4; device++)
            {
                names.Add("MidiDevice_" + device.ToString());
            }

            callback(names, null);
        }
    }
}
