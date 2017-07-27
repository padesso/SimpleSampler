using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Error
{
    /// <summary>
    /// Basic exception fired when there is an error getting Midi Devices.
    /// </summary>
    [Serializable]
    public class MidiDeviceServiceException : Exception
    {
        public MidiDeviceServiceException(string message) : base(message)
        {
        }
    }
}
