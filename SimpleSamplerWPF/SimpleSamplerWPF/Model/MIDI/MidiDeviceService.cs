using NAudio.Midi;
using SimpleSamplerWPF.Error;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            MidiInCapabilities cap = new MidiInCapabilities();

            if (MidiIn.NumberOfDevices > 0)
            {
                cap = MidiIn.DeviceInfo(deviceID);

                callback(cap, null);
            }
            else
            {
                MidiInCapabilities nullCap = new MidiInCapabilities();            
                callback(nullCap, new MidiDeviceServiceException("No Midi Devices Found"));
            }
        }

        //TODO: error handling
        public void GetDeviceNames(Action<ObservableCollection<string>, Exception> callback)
        {
            ObservableCollection<string> names = new ObservableCollection<string>();

            for (int device = 0; device < MidiIn.NumberOfDevices; device++)
            {
                names.Add(MidiIn.DeviceInfo(device).ProductName);
            }
                        
            callback(names, null);
        }
    }
}
