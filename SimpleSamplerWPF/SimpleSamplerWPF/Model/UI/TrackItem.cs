using SimpleSamplerWPF.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Model.UI
{
    public class TrackItem
    {
        #region Members
        private float volume;
        private float pan;
        private string name;
        private int noteNumber;
        Sample sample;
        #endregion

        #region Constructors
        public TrackItem(float volume = 0.5f, float pan = 0.5f, string name = "")
        {
            this.volume = volume;
            this.pan = pan;
            this.name = name;
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

        internal Sample Sample
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
