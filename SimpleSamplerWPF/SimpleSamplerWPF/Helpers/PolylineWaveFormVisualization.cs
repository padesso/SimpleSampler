using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Helpers
{
    class PolylineWaveFormVisualization : IVisualization
    {
        //private readonly PolylineWaveFormControl polylineWaveFormControl = new PolylineWaveFormControl();

        public string Name => "Polyline WaveForm Visualization";

        public object Content => null;// polylineWaveFormControl;

        public void OnMaxCalculated(float min, float max)
        {
            //polylineWaveFormControl.AddValue(max, min);
        }

        public void OnFftCalculated(NAudio.Dsp.Complex[] result)
        {
            // nothing to do
        }
    }
}
