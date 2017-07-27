using GalaSoft.MvvmLight.Messaging;
using NAudio.Midi;
using SimpleSamplerWPF.Model.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSamplerWPF.Logic
{
    /// <summary>
    /// Handles the receiving of Midi input and broadcasts messages based on that input.
    /// </summary>
    public class MidiEngine
    {
        private MidiIn midiIn;

        public MidiEngine()
        {

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
                    midiIn.Stop();

                    //Reset the event handlers
                    midiIn.MessageReceived -= MidiIn_MessageReceived;
                    midiIn.ErrorReceived -= MidiIn_ErrorReceived;

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
            //TODO: add other cases for dials, pitch wheel, etc...
            switch(e.MidiEvent.CommandCode)
            {
                case MidiCommandCode.NoteOn:
                    Messenger.Default.Send(((NoteOnEvent)e.MidiEvent));
                    break;

                default:
                    break;
            }
        }

        private void MidiIn_ErrorReceived(object sender, MidiInMessageEventArgs e)
        {
            // Show errr message
            Console.WriteLine("MIDI In Error: " + e.RawMessage);
        }
    }
}
