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

        //TEST ONLY
        CachedSound boom = new CachedSound(@"TestAudio\CYCdh_K1close_Kick-01.wav");

        public SamplerEngine()
        {
            tracks = new List<TrackItem>();
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
                    //Reset the event handlers
                    midiIn.MessageReceived -= MidiIn_MessageReceived;
                    midiIn.ErrorReceived += MidiIn_ErrorReceived;

                    midiIn.Dispose();
                    midiIn = null;
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

            //Only allow note on events for now - TODO: convert to switch statement
            if (e.MidiEvent.CommandCode != MidiCommandCode.NoteOn)
                return;

            //TODO: this should be a dictionary or something with a better fetch time
            for (int trackIndex = 0; trackIndex < tracks.Count; trackIndex++)
            {
                Console.WriteLine(tracks[trackIndex].NoteNumber);

                if(tracks[trackIndex].NoteNumber == ((NoteOnEvent)e.MidiEvent).NoteNumber)
                {
                    //TODO: play sound
                }
            }

            
            AudioPlaybackEngine.Instance.PlaySound(boom);
        }

        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            //TODO: show errr message...
        }
    }
}
