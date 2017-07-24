using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using static SimpleSamplerWPF.ViewModel.TrackControlViewModel;

namespace SimpleSamplerWPF.Converters
{
    public class LearnModeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
           System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be brush.");

            //TODO: use global styling
            if ((LearnModes)value == LearnModes.MidiLearnMode)
            {                
                return Brushes.DarkRed;
            }
            else if ((LearnModes)value == LearnModes.SampleLearnMode)
            {
                return Brushes.DarkBlue;
            }
            else if ((LearnModes)value == LearnModes.None)
            {
                return Brushes.WhiteSmoke;
            }
            else
            {
                throw new InvalidOperationException("Unexpected learn mode.");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
