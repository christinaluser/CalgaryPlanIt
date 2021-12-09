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
    /// Interaction logic for ViewList.xaml
    /// </summary>
    public partial class ViewList : Page
    {
        Lis Lis;
        string SortType = "A-Z";
        public ViewList()
        {
            InitializeComponent();
        }

        public ViewList(Lis lis)
        {
            InitializeComponent();
            Lis = lis;
            Lis.Attractions = Lis.Attractions?.OrderBy(t => t.Name).ToList();
            RefreshAttractionsGrid(Lis.Attractions);
            Name.Text = Lis.Name;
        }

        public void RefreshAttractionsGrid(List<Attraction> attractions)
        {
            AttractionsList.Children.Clear();
            if (Lis.Attractions?.Count > 0)
            {
                foreach (Attraction attraction in attractions)
                {
                    var card = new ListAttractionCard(attraction);
                    card.ArchiveButtonClicked += Trip_ArchiveButtonClicked;
                    AttractionsList.Children.Add(card);
                }
            } else
            {
                AttractionsList.Children.Add(new TextBlock() { Text = "No items in this list"});
            }
            
        }

        private void Trip_ArchiveButtonClicked(object sender, EventArgs e)
        {
            var index = MainWindow.ListofLists.FindIndex(list => Lis == list);
            MainWindow.ListofLists[index].Attractions?.Remove(((Attraction)sender));
            Lis.Attractions?.Remove(((Attraction)sender));
            RefreshAttractionsGrid(Lis.Attractions);
        }

        private void Sort()
        {
            if (Lis == null || Lis?.Attractions?.Count < 0) return;
            List<Attraction> templist = new List<Attraction>();
            if (SortType == "A-Z")
            {
                templist = Lis.Attractions.OrderBy(t => t.Name).ToList();
            }
            else if (SortType == "Z-A")
            {
                templist = Lis.Attractions.OrderByDescending(t => t.Name).ToList();
            }
            else if (SortType == "Price: High")
            {
                templist = Lis.Attractions.OrderByDescending(t => t.Price).ToList();
            }
            else if (SortType == "Price: Low")
            {
                templist = Lis.Attractions.OrderBy(t => t.Price).ToList();
            }
            else if (SortType == "Date")
            {
                templist = Lis.Attractions.OrderBy(t => t.StartDate).ToList();
            }

            if (AttractionsList != null && AttractionsList.Children.Count > 0)
            RefreshAttractionsGrid(templist);
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var split = ((ComboBox)sender).SelectedItem.ToString().Split(" ");
            SortType = split[1];
            if (split.Length > 2)
                SortType += " " + split[2];
            Sort();
            
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var searchVal = SearchBox.Text;
                List<Attraction> searchResults = Lis.Attractions.FindAll(t => t.Name.Contains(searchVal, StringComparison.OrdinalIgnoreCase));
                SearchResultsTitle.Text += "\"" + searchVal + "\"    (" + searchResults.Count + " results)";
                SearchHeader.Visibility = Visibility.Visible;
                RefreshAttractionsGrid(searchResults);
            }
        }

        private void ClearSearchResults(object sender, EventArgs e)
        {
            SearchBox.Clear();
            SearchResultsTitle.Text = "Search results for ";
            SearchHeader.Visibility = Visibility.Collapsed;
            RefreshAttractionsGrid(Lis.Attractions);
        }
    }
}
