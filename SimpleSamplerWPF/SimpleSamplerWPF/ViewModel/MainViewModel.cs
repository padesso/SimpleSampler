using GalaSoft.MvvmLight;
using SimpleSamplerWPF.Model;
using SimpleSamplerWPF.Model.MIDI;
using System.Collections.ObjectModel;

namespace SimpleSamplerWPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IMidiDeviceService _midiDeviceService;

        private ObservableCollection<string> midiDevices;

        public ObservableCollection<string> MidiDevices
        {
            get
            {
                return midiDevices;
            }

            set
            {
                Set(ref midiDevices, value);
            }
        }


        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IMidiDeviceService midiDeviceService)
        {
            _midiDeviceService = midiDeviceService;
            _midiDeviceService.GetDeviceNames(
                (names, error) =>
            {
                MidiDevices = names;

                //TEST ONLY
                MidiDevices = new ObservableCollection<string>() { "test", "test1" };
            });

            //TODO: pass selected index of device dropdown
            _midiDeviceService.GetDevice(0,
                (device, error) =>
                {
                    //TODO: set a value and bind to UI
                    var selectedDevice = device;
                });
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}