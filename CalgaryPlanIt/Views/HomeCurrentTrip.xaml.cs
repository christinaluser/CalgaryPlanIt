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
        bool IsCurrent = false;
        ItineraryItem Next;
        public HomeCurrentTrip()
        {
            InitializeComponent();
            var sorted = MainWindow.TripsList.OrderBy(t=>t.StartDate).Where(t=> t.EndDate > DateTime.Now && t.IsArchived == false).ToList();
            Trip = sorted.FirstOrDefault();
            if(Trip == null)
            {
                if (Navigation.window.Width > 450)
                    Navigation.NavigateTo(new Home());
                else 
                    Navigation.NavigateToMobile(new Home());
            }
            if(Trip.StartDate < DateTime.Now)
            {
                IsCurrent = true;
            }
            Next = FindUpcoming();
            SetContent();
            
        }
        public HomeCurrentTrip(bool isMobile)
        {
            InitializeComponent();
            var sorted = MainWindow.TripsList.OrderBy(t=>t.StartDate).Where(t=> t.EndDate > DateTime.Now && t.IsArchived == false).ToList();
            Trip = sorted.FirstOrDefault();
            if(Trip == null)
            {
                if (Navigation.window.Width > 450)
                    Navigation.NavigateTo(new Home());
                else 
                    Navigation.NavigateToMobile(new Home());
            }
            if(Trip.StartDate < DateTime.Now)
            {
                IsCurrent = true;
            }
            Next = FindUpcoming();
            SetContent();
            
        }

        private ItineraryItem FindUpcoming()
        {
            if (Trip.ItineraryItems == null || Trip.ItineraryItems?.Count == 0)
                return null;

            var n = Trip.ItineraryItems.FirstOrDefault();
            foreach(ItineraryItem item in Trip.ItineraryItems)
            {
                if (n.PlannedStartDate < item.PlannedStartDate && n.PlannedStartDate < DateTime.Now)
                {
                    n = item;
                }
            }
            if (n.PlannedStartDate < DateTime.Now)
                return null;
            return n;
        }

        private void SetContent()
        {
            TripName.Text = Trip.Name;
            TripDates.Text = Trip.TripDatesToString();
            NumTravellers.Text = Trip.GetNumTravellersString();

            if (Trip.ItineraryItems != null && Trip.ItineraryItems.Count > 0)
            {
                Itinerary.Children.Clear();
                DateTime d = new DateTime(Trip.StartDate.Year, Trip.StartDate.Month, Trip.StartDate.Day);
                if (d < DateTime.Now) d = DateTime.Now;
                while (DateTime.Compare(d.Date, Trip.EndDate.Date) <= 0)
                {
                    List<ItineraryItem> itineraryList = Trip.ItineraryItems.FindAll(i => i.PlannedStartDate.Date.Equals(d.Date));
                    
                    Itinerary.Children.Add(new ItineraryDayList(d, itineraryList, false));
                    d = d.AddDays(1);
                }
            }
            else
            {
                NoPlan.Visibility = Visibility.Visible;
            }

            if (Next != null)
            {
                Upcoming.Text = Next.Name;
                UpcomingTime.Text = Next.PlannedStartDate.ToString("t") + " - " + Next.PlannedEndDate.ToString("t");
            } else
            {
                UpcomingThing.Visibility = Visibility.Collapsed;
            }
        }

        private void HandleBack_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateTo(new Trips());
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(new ViewTrip(Trip), "My First Print Job");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Navigation.window.Width > 450)
                Navigation.NavigateTo(new Plan(Trip));
            else
                Navigation.NavigateToMobile(new MobilePlan(Trip));
        }
    }
}
