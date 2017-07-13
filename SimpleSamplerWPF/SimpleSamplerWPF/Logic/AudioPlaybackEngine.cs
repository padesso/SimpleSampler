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
        private readonly MixingSampleProviderFFT mixer;

        public event EventHandler<FftEventArgs> FftCalculated;
        public event EventHandler<MaxSampleEventArgs> MaximumCalculated;

        public AudioPlaybackEngine(int sampleRate = 44100, int channelCount = 8)
        {
            outputDevice = new DirectSoundOut();
            mixer = new MixingSampleProviderFFT(WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channelCount));
            mixer.ReadFully = true;
            outputDevice.Init(mixer);
            outputDevice.Play();
        }

        public void PlaySound(string fileName)
        {
            var input = new AudioFileReader(fileName);
            AddMixerInput(new AutoDisposeFileReader(input));
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

        public void PlaySound(CachedSound sound)
        {
            AddMixerInput(new CachedSoundSampleProvider(sound));
        }

        private void AddMixerInput(ISampleProvider input)
        {
            mixer.AddMixerInput(ConvertToRightChannelCount(input));
        }

        public void Dispose()
        {
            outputDevice.Dispose();
        }

        public void ReadWaveForm(CachedSound sample)
        {
            try
            {                
                mixer.NotificationCount = sample.WaveFormat.SampleRate / 100;
                mixer.PerformFFT = true;
                mixer.FftCalculated += (s, a) => FftCalculated?.Invoke(this, a);
                mixer.MaximumCalculated += (s, a) => MaximumCalculated?.Invoke(this, a);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Problem reading sample");
                //CloseFile();
            }
        }
        
        //TODO: This is probably causing the issue with event handlers disappearing
        public static readonly AudioPlaybackEngine Instance = new AudioPlaybackEngine(44100, 2);
    }
}
