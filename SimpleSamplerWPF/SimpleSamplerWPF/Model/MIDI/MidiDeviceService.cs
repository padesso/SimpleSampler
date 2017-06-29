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
        //TODO: error handling
        public void GetDevice(int deviceID, Action<MidiInCapabilities, Exception> callback)
        {
            var device = MidiIn.DeviceInfo(deviceID);

            callback(device, null);
        }

        //TODO: error handling
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
