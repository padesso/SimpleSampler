using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Logic
{
    public class Track
    {
        #region Enum
        public enum PlaybackTypes
        {
            Hit,
            Loop,
            Hold
        };
        #endregion

        #region Members
        private float volume;
        private bool muted;
        private bool soloed;
        private float pan;
        private string name;
        private PlaybackTypes playbackType;
        private int noteNumber;
        CachedSound sample;
        #endregion

        #region Constructors
        public Track(float volume = 0.5f, bool muted = false, bool soloed = false, float pan = 0.5f, string name = "", PlaybackTypes playbackType = PlaybackTypes.Hit)
        {
            this.volume = volume;
            this.muted = muted;
            this.soloed = soloed;
            this.pan = pan;
            this.name = name;
            this.playbackType = playbackType;
        }
        #endregion

        #region Parameters
        public float Volume
        {
            get
            {
                return volume;
            }

            set
            {
                if (value < 0.0f)
                {
                    volume = 0.0f;
                }
                else if (value > 1.0f)
                {
                    volume = 1.0f;
                }
                else
                {
                    volume = value;
                }
            }
        }

        public bool Muted
        {
            get
            {
                return muted;
            }

            set
            {
                muted = value;
            }
        }

        public bool Soloed
        {
            get
            {
                return soloed;
            }

            set
            {
                soloed = value;
            }
        }

        public float Pan
        {
            get
            {
                return pan;
            }

            set
            {
                if (value < 0.0f)
                {
                    pan = 0.0f;
                }
                else if (value > 1.0f)
                {
                    pan = 1.0f;
                }
                else
                {
                    pan = value;
                }
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

        public int NoteNumber
        {
            get
            {
                return noteNumber;
            }

            set
            {
                noteNumber = value;
            }
        }

        internal CachedSound Sample
        {
            get
            {
                return sample;
            }

            set
            {
                sample = value;
            }
        }
        #endregion
    }
}
