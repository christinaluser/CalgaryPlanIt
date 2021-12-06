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
        public event EventHandler TripClicked;

        public TripCard()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public TripCard(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            SetContent();
        }

        private void SetContent()
        {
            TripName.Text = Trip.Name;
            Date.Text = Trip.TripDatesToString();
            NumTravellers.Text = Trip.GetNumTravellersString();
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
            //Navigation.NavigateTo(new ViewTrip(Trip));
            TripClicked.Invoke(Trip, e);
        }
    }
}
