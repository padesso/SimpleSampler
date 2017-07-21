using SimpleSamplerWPF.ViewModel;
using System.Windows.Controls;

namespace SimpleSamplerWPF.Controls
{
    /// <summary>
    /// Interaction logic for VisualizerControl.xaml
    /// </summary>
    public partial class VisualizerControl : UserControl
    {
        public VisualizerControl()
        {
            InitializeComponent();
            DataContext = new VisualizerViewModel();
        }
    }
}
