using NAudio.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.MIDI
{
    /// <summary>
    /// Model for Midi devices.
    /// </summary>
    public class MidiDevicesItem
    {
        MidiIn midiInputDevices;

        List<string> deviceNames;
        MidiInCapabilities deviceInfo;

        public MidiDevicesItem(MidiIn midiInputDevices)
        {
            this.MidiInputDevices = midiInputDevices;
        }

        public MidiIn MidiInputDevices
        {
            get
            {
                return midiInputDevices;
            }

            private set
            {
                midiInputDevices = value;
            }
        }

        public List<string> DeviceNames
        {
            get
            {
                return deviceNames;
            }

            private set
            {
                deviceNames = value;
            }
        }

        public MidiInCapabilities DeviceInfo
        {
            get
            {
                return deviceInfo;
            }

            private set
            {
                deviceInfo = value;
            }
        }
    }
}
