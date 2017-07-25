using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using SimpleSamplerWPF.Helpers;
using SimpleSamplerWPF.Helpers.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleSamplerWPF.Logic
{
    class AudioPlaybackEngine : IDisposable
    {
        private readonly IWavePlayer outputDevice;
        private readonly MixingSampleProvider mixer;

        VolumeSampleProvider volumeProvider;
        PanningSampleProvider panProvider;  //TODO: implement this...

        private float masterVolume;

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

        public void PlaySound(CachedSound sound, float volume, bool isPreview)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound), volume, isPreview);
        }

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

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);

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
