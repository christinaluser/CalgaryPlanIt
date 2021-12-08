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

namespace CalgaryPlanIt.Components
{
    /// <summary>
    /// Interaction logic for MapMarker.xaml
    /// </summary>
    public partial class MapMarker : UserControl
    {
        public event EventHandler MapMarkerClicked; 
        bool HoverForLabel;
        
        public MapMarker()
        {
            InitializeComponent();
        }

        public MapMarker(string label, Brush? color, bool hoverForLabel = false)
        {
            InitializeComponent();
            HoverForLabel = hoverForLabel;
            MarkerLabelText.Text = label;
            MarkerLabel.Background = color ?? Brushes.AliceBlue;
            MarkerLabel.Visibility = hoverForLabel ? Visibility.Hidden : Visibility.Visible;
            if (label.Equals("You"))
            {
                MarkerIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/MapPin.png"));
            }
            
        }
        int z;
        private void MarkerIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            if (HoverForLabel)
            {
                
                MarkerLabel.Visibility = Visibility.Visible;
            }
        }

        private void MarkerIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            if (HoverForLabel)
            {

                MarkerLabel.Visibility = Visibility.Hidden;
            }
        }

        private void MarkerIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MapMarkerClicked.Invoke(MarkerLabelText.Text, e);
        }
    }
}
