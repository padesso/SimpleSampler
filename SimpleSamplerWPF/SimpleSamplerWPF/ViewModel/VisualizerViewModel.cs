using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using NAudio.Wave;
using SimpleSamplerWPF.Helpers;
using SimpleSamplerWPF.Helpers.Events;
using SimpleSamplerWPF.Logic;
using SimpleSamplerWPF.Model.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

// Visualizer converted to WPF and MVVM based on:
// http://www.giawa.com/tutorials/


namespace SimpleSamplerWPF.ViewModel
{
    public class VisualizerViewModel : ViewModelBase
    {
        private Sample selectedSample;

        public RelayCommand PlaySampleCommand { get; private set; }
        public RelayCommand<RoutedEventArgs> CanvasLoadedCommand { get; private set; }
        public RelayCommand<SizeChangedEventArgs> CanvasSizeChangedCommand { get; private set; }

        PointCollection polylinePoints;

        private double canvasHeight;
        private double canvasWidth;

        public VisualizerViewModel()
        {           
            PlaySampleCommand = new RelayCommand(PlaySample, IsSampleLoaded);
            CanvasLoadedCommand = new RelayCommand<RoutedEventArgs>(CanvasLoaded);
            CanvasSizeChangedCommand = new RelayCommand<SizeChangedEventArgs>(CanvasSizeChanged);
            
            Messenger.Default.Register<Sample>(this, m => SelectedSample = m);            
        }

        /// <summary>
        /// Gets the initial height of the visualizer canvas.
        /// </summary>
        /// <param name="args">Parameters of the visualizer canvas.</param>
        private void CanvasLoaded(RoutedEventArgs args)
        {
            canvasHeight = ((Canvas)args.Source).ActualHeight;
            canvasWidth = ((Canvas)args.Source).ActualWidth;
        }

        /// <summary>
        /// Called when the canvas is resized.
        /// </summary>
        /// <param name="args">Parameters of the resizing of the visualizer canvas.</param>
        private void CanvasSizeChanged(SizeChangedEventArgs args)
        {
            canvasHeight = ((Canvas)args.Source).ActualHeight;
            canvasWidth = ((Canvas)args.Source).ActualWidth;

            drawWaveform();
        }

        /// <summary>
        /// Play the selected sample.
        /// </summary>
        public void PlaySample()
        {
            if (selectedSample != null)
                AudioPlaybackEngine.Instance.PlaySound(SelectedSample.CachedSound, 1.0f, true);
        }

        private bool IsSampleLoaded()
        {
            if (selectedSample == null)
                return false;

            return true;
        }

        internal Sample SelectedSample
        {
            get
            {
                return selectedSample;
            }

            set
            {
                Set("SelectedSample", ref selectedSample, value);

                PlaySampleCommand.RaiseCanExecuteChanged(); //Notify the UI for play button state

                drawWaveform(); 
            }
        }

        public PointCollection PolylinePoints
        {
            get
            {
                return polylinePoints;
            }

            set
            {
                Set("PolylinePoints", ref polylinePoints, value);
            }
        }

        /// <summary>
        /// Draw the waveform scaled to the canvas size.  If the sample is null, clear the waveform.
        /// </summary>
        private void drawWaveform()
        {
            polylinePoints = new PointCollection();

            if (selectedSample != null)
            {
                float[] data = selectedSample.CachedSound.AudioData;

                double sampleStepRatio = canvasWidth / data.Length;

                // move to center of visualizer, scale vertically and horizontally.                
                for (int sampleIndex = 0; sampleIndex < data.Length; sampleIndex++)
                {
                    polylinePoints.Add(new Point(sampleIndex * sampleStepRatio, (data[sampleIndex] * (canvasHeight / 2)) + (canvasHeight / 2)));
                }
            }

            RaisePropertyChanged("PolylinePoints");
        }
    }
}
