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
    /// Interaction logic for TripCard.xaml
    /// </summary>
    public partial class TripCard : UserControl
    {
        public Trip Trip;
        public TripCard()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public TripCard(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            TripName.Content = Trip.Name;
            Date.Content = Trip.TripDatesToString();
            NumTravellers.Children.Clear();
            if (Trip.NumAdults > 0)
                NumTravellers.Children.Add(new Label() { Content = Trip.NumAdults + (Trip.NumAdults == 1 ? "Adult" : " Adults") });
            if (Trip.NumTeens > 0)
                NumTravellers.Children.Add(new Label() { Content = Trip.NumTeens + (Trip.NumTeens == 1 ? "Teen" : "Teens") });
            if (Trip.NumChildren > 0)
                NumTravellers.Children.Add(new Label() { Content = Trip.NumChildren + (Trip.NumChildren == 1 ? "Child" : "Children") });

        }
    }
}
