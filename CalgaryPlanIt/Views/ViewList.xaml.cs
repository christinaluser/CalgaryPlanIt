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
    /// Interaction logic for ViewList.xaml
    /// </summary>
    public partial class ViewList : Page
    {
        string SortType = "A-Z";
        public ViewList()
        {
            InitializeComponent();
        }
        public void RefreshTripsGrid(List<Trip> TripList)
        {
            AttractionsList.Children.Clear();
            foreach (Trip trip in TripList)
            {
                if (!trip.IsArchived)
                {
                    //var card = new TODO(trip);
                    //card.ArchiveButtonClicked += Trip_ArchiveButtonClicked;
                    //AttractionsList.Children.Add(card);
                }
            }
        }

        private void Trip_ArchiveButtonClicked(object sender, EventArgs e)
        {
            //var index = MainWindow.TripsList.FindIndex(trip => ((TODO)sender).Trip == trip);
            //MainWindow.TripsList[index].IsArchived = true;
            //RefreshTripsGrid(MainWindow.TripsList);
        }

        private void Sort()
        {
            if (SortType == "A-Z")
            {
                MainWindow.TripsList = MainWindow.TripsList.OrderBy(t => t.Name).ToList();
            }
            else if (SortType == "Z-A")
            {
                MainWindow.TripsList = MainWindow.TripsList.OrderByDescending(t => t.Name).ToList();
            }
            else if (SortType == "Upcoming Trips")
            {
                MainWindow.TripsList = MainWindow.TripsList.OrderBy(t => t.StartDate).ToList();
                var temp = MainWindow.TripsList.FindAll(t => t.StartDate >= DateTime.Now);
                var temp2 = MainWindow.TripsList.FindAll(t => t.StartDate < DateTime.Now);
                MainWindow.TripsList = temp;
                foreach (Trip t in temp2)
                {
                    MainWindow.TripsList.Add(t);
                }

            }
            else if (SortType == "Closest Date")
            {
                MainWindow.TripsList = MainWindow.TripsList.OrderBy(t => Math.Abs(DateTime.Compare(t.StartDate, DateTime.Now))).ToList();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var split = ((ComboBox)sender).SelectedItem.ToString().Split(" ");
            SortType = split[1];
            if (split.Length > 2)
                SortType += " " + split[2];
            Sort();
            if (AttractionsList != null && AttractionsList.Children.Count > 0)
                RefreshTripsGrid(MainWindow.TripsList);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var searchVal = SearchBox.Text;
                List<Trip> searchResults = MainWindow.TripsList.FindAll(t => t.Name.Contains(searchVal));
                SearchResultsTitle.Text += "\"" + searchVal + "\"    (" + searchResults.Count + " results)";
                SearchHeader.Visibility = Visibility.Visible;
                RefreshTripsGrid(searchResults);
            }
        }

        private void ClearSearchResults(object sender, EventArgs e)
        {
            SearchBox.Clear();
            SearchHeader.Visibility = Visibility.Collapsed;
            RefreshTripsGrid(MainWindow.TripsList);
        }
    }
}
