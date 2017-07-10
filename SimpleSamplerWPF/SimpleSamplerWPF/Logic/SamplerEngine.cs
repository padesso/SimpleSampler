using NAudio.Midi;
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
        private MidiIn midiIn;

        private List<TrackItem> tracks;
        private List<CachedSound> samples;
        
        public SamplerEngine()
        {

        }

        public List<TrackItem> Tracks
        {
            get
            {
                return tracks;
            }

            set
            {
                tracks = value;
            }
        }

        public List<CachedSound> Samples
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

        public MidiIn MidiIn
        {
            get
            {
                return midiIn;
            }

            set
            {
                //Clean up the existing input
                if (midiIn != null)
                {
                    midiIn.Dispose();
                    midiIn = null;

                    //Reset the event handlers
                    midiIn.MessageReceived -= MidiIn_MessageReceived;
                    midiIn.ErrorReceived += MidiIn_ErrorReceived;
                }

                midiIn = value;
                
                //Capture the input for the newly selected Midi In
                midiIn.MessageReceived += MidiIn_MessageReceived;
                midiIn.ErrorReceived += MidiIn_ErrorReceived;

                //Start monitoring for events with the new selected Midi input.
                midiIn.Start();
            }
        }
        
        private void MidiIn_MessageReceived(object sender, MidiInMessageEventArgs e)
        {
            //TODO: Route events to all tracks and play if appropriate.
            Console.WriteLine("Midi In: " + e.MidiEvent.ToString());
        }

        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            //TODO: show errr message...
        }
    }
}
