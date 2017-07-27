using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SimpleSamplerWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleSamplerWPF.Logic
{
    /// <summary>
    /// Singleton audio playback engine.
    /// </summary>
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        VolumeSampleProvider volumeProvider;
        //PanningSampleProvider panProvider;  //TODO: implement this...

        private float masterVolume;

        /// <summary>
        /// Contstructor to create the audio engine.
        /// </summary>
        /// <param name="sampleRate">Frequency of sample rate.</param>
        /// <param name="channelCount">Number of channels.</param>
        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 2)
        {
            outputDevice = new DirectSoundOut();
            mixer = new MixingSampleProvider(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;

            volumeProvider = new VolumeSampleProvider(mixer);
            //panProvider = new PanningSampleProvider(volumeProvider);

            outputDevice.Init(volumeProvider);
            outputDevice.Play();            
        }

        /// <summary>
        /// Converts sample provided to match that of the waveformat in use.
        /// </summary>
        /// <param name="input">The sample provider to convert.</param>
        /// <returns></returns>
        private ISampleProvider ConvertToRightChannelCount(ISampleProvider input)
        {
            if (input.WaveFormat.Channels == mixer.WaveFormat.Channels)
            {
                return input;
            }
            if (input.WaveFormat.Channels == 1 && mixer.WaveFormat.Channels == 2)
            {
                return new MonoToStereoSampleProvider(input);
            }
            throw new NotImplementedException("Not yet implemented this channel count conversion");
        }

        /// <summary>
        /// Play a sound.
        /// </summary>
        /// <param name="sound">A CachedSound to play.</param>
        /// <param name="volume">What volume to play the sound (0.0 - 1.0)</param>
        /// <param name="isPreview">Is this sound being played as a preview? If so, ignore volume.</param>
        public void PlaySound(CachedSound sound, float volume, bool isPreview)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound), volume, isPreview);
        }

        /// <summary>
        /// Adds a mixer input to the mixing sample provider for playback.
        /// </summary>
        /// <param name="input">Sample provider.</param>
        /// <param name="volume">What volume to play the sound (0.0 - 1.0)</param>
        /// <param name="isPreview">Is this sound being played as a preview? If so, ignore volume.</param>
        private void AddMixerInput(ISampleProvider input, float volume, bool isPreview)
        {
            //If this is a preview sound, we want full volume regardless of 
            //track of master volume setting.
            if (isPreview)
            {
                volumeProvider.Volume = 1.0f;
                mixer.AddMixerInput(ConvertToRightChannelCount(input));
            }
            else
            {
                volumeProvider.Volume = volume * MasterVolume;
                mixer.AddMixerInput(ConvertToRightChannelCount(input));
            }
        }

        /// <summary>
        /// Get rid of the output.
        /// </summary>
        public void Dispose()
        {
            outputDevice.Dispose();
        }

        /// <summary>
        /// Singleton pattern for the audio engine.
        /// </summary>
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);

        /// <summary>
        /// Master volume is multiplied by track volume to get total volume.  Ignored for preview playback.
        /// </summary>
        public float MasterVolume
        {
            get
            {
                return masterVolume;
            }

            set
            {
                masterVolume = value;
            }
        }
    }
}
