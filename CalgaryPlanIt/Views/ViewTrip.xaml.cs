using CalgaryPlanIt.Components;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CalgaryPlanIt.Views
{
    /// <summary>
    /// Interaction logic for ViewTrip.xaml
    /// </summary>
    public partial class ViewTrip : Page
    {
        Trip Trip;
        public ViewTrip()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public ViewTrip(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            SetContent();
            SubscribeToAllReviewButtonEvent();
            if (Navigation.window.Width <= 450)
            {
                sv.Height = 560;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (Navigation.window.Width > 450)
                Navigation.NavigateTo(new Plan(Trip));
            else
                Navigation.NavigateToMobile(new MobilePlan(Trip));
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
                if (Trip.StartDate == DateTime.MinValue && Trip.ItineraryItems != null)
                {
                    d = Trip.ItineraryItems[0].PlannedStartDate;
                    Trip.ItineraryItems.ForEach(i =>
                    {
                        if (i.PlannedStartDate < d)
                        {
                            d = i.PlannedStartDate;
                        }
                    });
                }

                if (Trip.EndDate != DateTime.MinValue)
                {
                    while (DateTime.Compare(d.Date, Trip.EndDate.Date) <= 0)
                    {
                        List<ItineraryItem> itineraryList = Trip.ItineraryItems.FindAll(i => i.PlannedStartDate.Date.Equals(d.Date));
                        Itinerary.Children.Add(new ItineraryDayList(d, itineraryList, true));
                        d = d.AddDays(1);
                    }
                } else
                {
                    DateTime e = Trip.ItineraryItems[0].PlannedStartDate;
                    Trip.ItineraryItems.ForEach(i =>
                    {
                        if (i.PlannedStartDate > e)
                        {
                            e = i.PlannedStartDate;
                        }
                    });
                    while (DateTime.Compare(d.Date, e) <= 0)
                    {
                        List<ItineraryItem> itineraryList = Trip.ItineraryItems.FindAll(i => i.PlannedStartDate.Date.Equals(d.Date));
                        Itinerary.Children.Add(new ItineraryDayList(d, itineraryList, true));
                        d = d.AddDays(1);
                    }
                }
                
            }
            else
            {
                NoPlan.Visibility = Visibility.Visible;
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
                foreach (ItineraryDayList itineraryDayList in Itinerary.Children)
                {
                    itineraryDayList.ReviewButtonClicked += ViewTrip_ReviewButtonClicked;
                }
            }

        }

        private void ViewTrip_ReviewButtonClicked(object sender, EventArgs e)
        {
            ReviewModal modal = new ReviewModal((Attraction)((Button)sender).Tag);
            modal.CloseButtonClicked += ViewTrip_CloseReviewButtonClicked;
            modal.HorizontalAlignment = HorizontalAlignment.Center;
            if (Navigation.window.Width <= 450)
            {
                modal.border.Width = Navigation.window.Width - 100;
            }
            Overlay.Children.Add(modal);
            Overlay.Visibility = Visibility.Visible;
        }

        private void ViewTrip_CloseReviewButtonClicked(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "My First Print Job");
            }
        }
    }
}
