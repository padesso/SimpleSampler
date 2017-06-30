using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Error
{
    [Serializable]
    public class MidiDeviceServiceException : Exception
    {
        public MidiDeviceServiceException(string message) : base(message)
        {
        }
    }
}
