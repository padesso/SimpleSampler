using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Logic
{
    public class Sample
    {
        private CachedSound cachedSound;
        private string name;

        public Sample(CachedSound cachedSound, string name)
        {
            this.CachedSound = cachedSound;
            this.Name = name;
        }

        public CachedSound CachedSound
        {
            get
            {
                return cachedSound;
            }

            set
            {
                cachedSound = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }
    }
}
