using SimpleSamplerWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleSamplerWPF.Controls
{
    /// <summary>
    /// Interaction logic for TrackControl.xaml
    /// </summary>
    public partial class TrackControl : UserControl
    {
        private bool isMaster = false;

        public TrackControl()
        {
            InitializeComponent();

            DataContext = new TrackControlViewModel();
        }

        public bool IsMaster
        {
            get
            {
                return isMaster;
            }

            set
            {
                isMaster = value;

                if (isMaster)
                {
                    trackNameText.IsEnabled = false;
                    trackNameText.Text = "Master";
                }
            }
        }
    }
}
