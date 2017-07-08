using SimpleSamplerWPF.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Logic
{
    public class SamplerEngine
    {
        private List<CachedSound> samples;
        private List<TrackItem> tracks;

        public SamplerEngine()
        {

        }

        internal List<CachedSound> Samples
        {
            get
            {
                return samples;
            }

            set
            {
                samples = value;
            }
        }
    }
}
