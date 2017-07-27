using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SimpleSamplerWPF.Logic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SimpleSamplerWPF.ViewModel
{
    /// <summary>
    /// View model for the sample visualizer.
    /// </summary>
    public class VisualizerViewModel : ViewModelBase
    {
        private Sample selectedSample;

        public RelayCommand PlaySampleCommand { get; private set; }
        public RelayCommand<RoutedEventArgs> CanvasLoadedCommand { get; private set; }
        public RelayCommand<SizeChangedEventArgs> CanvasSizeChangedCommand { get; private set; }

        PointCollection polylinePoints;

        private double canvasHeight;
        private double canvasWidth;

        /// <summary>
        /// Default, parameterless constructor
        /// </summary>
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

        /// <summary>
        /// Is a sample currently loaded.  Bound to play button enabled state.
        /// </summary>
        /// <returns></returns>
        private bool IsSampleLoaded()
        {
            if (selectedSample == null)
                return false;

            return true;
        }

        /// <summary>
        /// The currently selected Sample.
        /// </summary>
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

        /// <summary>
        /// The collection of points used to draw the waveform.
        /// </summary>
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
