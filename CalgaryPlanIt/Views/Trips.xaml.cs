﻿using CalgaryPlanIt.Components;
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
    /// Interaction logic for Trips.xaml
    /// </summary>
    public partial class Trips : Page
    {
        List<Trip> TripsList;

        public Trips()
        {
            InitializeComponent();
            CreateTrips();
            RefreshTripsGrid();
            SubcribeToAllArchiveTripCardEvent();
        }

        public void RefreshTripsGrid()
        {
            TripsGrid.Children.Clear();
            foreach (Trip trip in TripsList)
            {
                if (!trip.IsArchived)
                    TripsGrid.Children.Add(new TripCard(trip));
            }
        }

        private void SubcribeToAllArchiveTripCardEvent()
        {
            foreach (TripCard tripCard in this.TripsGrid.Children)
            {
                tripCard.ArchiveButtonClicked += Trip_ArchiveButtonClicked;
            }
        }

        private void Trip_ArchiveButtonClicked(object sender, EventArgs e)
        {
            var index = TripsList.FindIndex(trip => ((TripCard)sender).Trip == trip);
            TripsList[index].IsArchived = true;
            RefreshTripsGrid();
            SubcribeToAllArchiveTripCardEvent();
        }

        private void CreateTrips()
        {
            TripsList = new List<Trip>();
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(10),
                EndDate = DateTime.Now.AddDays(20),
                Name = "trip 1",
                NumAdults = 0,
                NumChildren = 0,
                NumTeens = 6
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(15),
                EndDate = DateTime.Now.AddDays(25),
                Name = "trip 2",
                NumAdults = 1,
                NumChildren = 3,
                NumTeens = 0
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(5),
                EndDate = DateTime.Now.AddDays(7),
                Name = "trip 3",
                NumAdults = 2,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(50),
                EndDate = DateTime.Now.AddDays(57),
                Name = "trip 4",
                NumAdults = 3,
                NumChildren = 1,
                NumTeens = 2
            });
            TripsList.Add(new Trip()
            {
                StartDate = DateTime.Now.AddDays(35),
                EndDate = DateTime.Now.AddDays(57),
                Name = "trip 5",
                NumAdults = 3,
                NumChildren = 1,
                NumTeens = 2,
                ItineraryItems = new List<ItineraryItem>() { 
                    new ItineraryItem() { Name = "Calgary tower", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cactus Club Cafe", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cibo", PlannedStartDate = DateTime.Now.AddDays(36)},
                    new ItineraryItem() { Name = "Cactus Club Cafe pt 2", PlannedStartDate = DateTime.Now.AddDays(37)},
                    new ItineraryItem() { Name = "Cibo pt 2", PlannedStartDate = DateTime.Now.AddDays(38)},
                }
            });
        }
    }
}
