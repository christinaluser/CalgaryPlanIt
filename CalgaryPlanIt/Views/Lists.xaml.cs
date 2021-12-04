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
    /// Interaction logic for Lists.xaml
    /// </summary>
    public partial class Lists : Page
    {
        string SortType = "A-Z";


        public Lists()
        {
            InitializeComponent();
            AddListCards(MainWindow.ListofLists);
        }

        private void AddListCards(List<Lis> ListofLists)
        {
            ListGrid.Children.Clear();
            foreach(Lis lis in ListofLists)
            {
                ListGrid.Children.Add(new ListCard(lis));
            }

        }

        private void Sort()
        {
            if (SortType == "A-Z")
            {
                MainWindow.ListofLists = MainWindow.ListofLists.OrderBy(t => t.Name).ToList();
            }
            else if (SortType == "Z-A")
            {
                MainWindow.ListofLists = MainWindow.ListofLists.OrderByDescending(t => t.Name).ToList();
            }
            
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var split = ((ComboBox)sender).SelectedItem.ToString().Split(" ");
            SortType = split[1];
            if (split.Length > 2)
                SortType += " " + split[2];
            Sort();
            if (ListGrid != null && ListGrid.Children.Count > 0)
                AddListCards(MainWindow.ListofLists);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var searchVal = SearchBox.Text;
                List<Lis> searchResults = MainWindow.ListofLists.FindAll(t => t.Name.Contains(searchVal));
                SearchResultsTitle.Text += "\"" + searchVal + "\"    (" + searchResults.Count + " results)";
                SearchHeader.Visibility = Visibility.Visible;
                AddListCards(searchResults);
            }
        }

        private void ClearSearchResults(object sender, EventArgs e)
        {
            SearchBox.Clear();
            SearchHeader.Visibility = Visibility.Collapsed;
            AddListCards(MainWindow.ListofLists);
        }
    }
}
