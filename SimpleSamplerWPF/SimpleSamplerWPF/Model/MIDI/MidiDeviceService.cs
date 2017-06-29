using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.MIDI
{
    class MidiDeviceService : IMidiDeviceService
    {
        public void GetDevice(Action<MidiInCapabilities, Exception> callback)
        {
            
        }

        public void GetDeviceNames(Action<List<string>, Exception> callback)
        {
            List<string> names = new List<string>();

            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                names.Add(MidiIn.DeviceInfo(device).ProductName);
            }
                        
            callback(names, null);
        }
    }
}
