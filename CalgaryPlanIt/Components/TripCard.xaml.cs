using CalgaryPlanIt.Views;
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
        public event EventHandler ArchiveButtonClicked;
        public TripCard()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public TripCard(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            TripName.Text = Trip.Name;
            Date.Text = Trip.TripDatesToString();
            NumTravellers.Children.Clear();
            if (Trip.NumAdults > 0)
                NumTravellers.Children.Add(new TextBlock() { Text = Trip.NumAdults + (Trip.NumAdults == 1 ? " Adult" : " Adults") });
            if (Trip.NumTeens > 0)
                NumTravellers.Children.Add(new TextBlock() { Text = Trip.NumTeens + (Trip.NumTeens == 1 ? " Teen" : " Teens") });
            if (Trip.NumChildren > 0)
                NumTravellers.Children.Add(new TextBlock() { Text = Trip.NumChildren + (Trip.NumChildren == 1 ? " Child" : " Children") });
        }

        private void HandleArchive_Click(object sender, RoutedEventArgs e)
        {
            ArchiveButtonClicked.Invoke(this, e);
        }

            private void HandlePlan_Click(object sender, RoutedEventArgs e)
        {
            Page PlanPage = new Plan(Trip);
            Navigation.NavigateTo(PlanPage);
        }

        private void HandleViewTrip_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateTo(new ViewTrip(Trip));
        }
    }
}
