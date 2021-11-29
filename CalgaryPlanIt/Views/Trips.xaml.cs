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
    /// Interaction logic for Trips.xaml
    /// </summary>
    public partial class Trips : Page
    {
        

        public Trips()
        {
            InitializeComponent();
            RefreshTripsGrid();
            SubcribeToAllArchiveTripCardEvent();
        }

        public void RefreshTripsGrid()
        {
            TripsGrid.Children.Clear();
            foreach (Trip trip in MainWindow.TripsList)
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
            var index = MainWindow.TripsList.FindIndex(trip => ((TripCard)sender).Trip == trip);
            MainWindow.TripsList[index].IsArchived = true;
            RefreshTripsGrid();
            SubcribeToAllArchiveTripCardEvent();
        }

        
    }
}
