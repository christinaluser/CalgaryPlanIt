using CalgaryPlanIt.Components;
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

namespace CalgaryPlanIt.Views
{
    /// <summary>
    /// Interaction logic for HomeCurrentTrip.xaml
    /// </summary>
    public partial class HomeCurrentTrip : Page
    {
        Trip Trip;
        public HomeCurrentTrip()
        {
            InitializeComponent();
        }

        private void SetContent()
        {
            TripName.Text = Trip.Name;
            Dates.Text = Trip.TripDatesToString();
            NumTravellers.Text = Trip.GetNumTravellersString();

            if (Trip.ItineraryItems != null && Trip.ItineraryItems.Count > 0)
            {
                Itinerary.Children.Clear();
                DateTime d = new DateTime(Trip.StartDate.Year, Trip.StartDate.Month, Trip.StartDate.Day);
                while (DateTime.Compare(d.Date, Trip.EndDate.Date) <= 0)
                {
                    List<ItineraryItem> itineraryList = Trip.ItineraryItems.FindAll(i => i.PlannedStartDate.Date.Equals(d.Date));
                    Itinerary.Children.Add(new ItineraryDayList(d, itineraryList, true));
                    d = d.AddDays(1);
                }
            }
            else
            {
                Itinerary.Children.Add(new TextBlock { Text = "nothing on your itinerary yet" });
            }
        }

        private void HandleBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateTo(new Trips());
        }

        private void SubscribeToAllReviewButtonEvent()
        {
            if (Trip.ItineraryItems != null && Trip.ItineraryItems.Count > 0)
            {
                
            }

        }

        
    }
}
