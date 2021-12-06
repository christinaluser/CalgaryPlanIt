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
    /// Interaction logic for ThingsToDoSubsection.xaml
    /// </summary>
    public partial class ThingsToDoSubsection : Page
    {
        Category Category;
        Tag SelectedTags = CalgaryPlanIt.Tag.None;
        List<Attraction> Attractions = new List<Attraction>();
        AttractionDetails? CurrentDetails;
        bool SwitchViewOnDetailsClose = false;

        //Sort
        string SortType = "";

        private Point origin;
        private Point start;

        public ThingsToDoSubsection(Category category)
        {
            InitializeComponent();
            Category = category;
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            SetContent();
        }

        public void SetContent()
        {
            CategoryName.Text = Category.ToFriendlyString();
            PopulateFilterTags();
            PopulateAttractionsList();
            SetMap();
        }

        private void SetMap() 
        {
            TransformGroup group = new TransformGroup();

            ScaleTransform xform = new ScaleTransform();
            group.Children.Add(xform);

            TranslateTransform tt = new TranslateTransform();
            group.Children.Add(tt);

            MapCanvas.RenderTransform = group;

            MapCanvas.MouseWheel += MapCanvas_MouseWheel;
            MapCanvas.MouseLeftButtonDown += MapCanvas_MouseLeftButtonDown;
            MapCanvas.MouseLeftButtonUp += MapCanvas_MouseLeftButtonUp;
            MapCanvas.MouseMove += MapCanvas_MouseMove;

            SetMapMarkers();
        }

        private void SetMapMarkers()
        {
            var mapMarker = new MapMarker("You", null);
            Canvas.SetTop(mapMarker, 100);
            Canvas.SetLeft(mapMarker, 100);
            MapCanvas.Children.Add(mapMarker);

            //var mapMarker2 = new MapMarker(Attractions[0].Name, null, true);
            //Canvas.SetTop(mapMarker2, 200);
            //Canvas.SetLeft(mapMarker2, 200);
            //MapCanvas.Children.Add(mapMarker2);
        }

        private void MapCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MapCanvas.ReleaseMouseCapture();
        }

        private void MapCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!MapCanvas.IsMouseCaptured) return;

            var tt = (TranslateTransform)((TransformGroup)MapCanvas.RenderTransform).Children.First(tr => tr is TranslateTransform);
            Vector v = start - e.GetPosition(border);
            tt.X = origin.X - v.X;
            tt.Y = origin.Y - v.Y;
        }

        private void MapCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var tt = (TranslateTransform)((TransformGroup)MapCanvas.RenderTransform).Children.First(tr => tr is TranslateTransform);
            start = e.GetPosition(border);
            origin = new Point(tt.X, tt.Y);
            MapCanvas.CaptureMouse();
        }

        private void MapCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TransformGroup transformGroup = (TransformGroup)MapCanvas.RenderTransform;
            ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

            double zoom = e.Delta > 0 ? .2 : -.2;
            if (MapCanvas.ActualWidth * (transform.ScaleX + zoom) < 130 || MapCanvas.ActualHeight * (transform.ScaleY + zoom) < 130) //don't zoom out too small.
                return;
            transform.ScaleX += zoom;
            transform.ScaleY += zoom;
            
        }

        private void PopulateFilterTags()
        {
            FilterTagsPanel.Children.Clear();
            Tag[] allTags = (Tag[])Enum.GetValues(typeof(Tag));
            foreach (Tag tag in allTags)
            {
                CheckBox checkbox = new CheckBox() { 
                    Content = tag.ToFriendlyString(),
                    Tag = tag
                };
                checkbox.Checked += new RoutedEventHandler(AddFilterTag);
                checkbox.Unchecked += new RoutedEventHandler(RemoveFilterTag);
                FilterTagsPanel.Children.Add(checkbox);
            }
        }

        private void PopulateAttractionsList()
        {
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                var card = new AttractionCard(attraction);
                card.AttractionCardClicked += AttractionCard_Clicked;
                AttractionsList.Children.Add(card);
            }
        }

        public void AddFilterTag(object sender, RoutedEventArgs e)
        {
            Tag t = (Tag)((CheckBox)sender).Tag;
            SelectedTags |= t;

            var temp = Attractions[1].Tags.HasFlag(SelectedTags);
        }

        public void RemoveFilterTag(object sender, RoutedEventArgs e)
        {
            Tag t = (Tag)((CheckBox)sender).Tag;
            var mask = ~t;
            SelectedTags &= mask;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateTo(new ThingsToDo());
        }

        private void SwitchViewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AttractionCard_Clicked(object sender, EventArgs e)
        {
            //map is not visible
            if (SwitchViewButton.IsChecked == false)
            {
                SwitchViewOnDetailsClose = true;
                SwitchViewButton.IsChecked = true;
            }
            
            if (CurrentDetails != null)
            {
                PageMainContent.Children.Remove(CurrentDetails);
            }
            var attraction = ((AttractionCard)sender).Attraction;
            var attractionDetails = new AttractionDetails(attraction);

            Grid.SetColumn(attractionDetails, 1);
            attractionDetails.AttractionDetailsCloseClicked += AttractionDetails_CloseClicked;
            CurrentDetails = attractionDetails;
            PageMainContent.Children.Add(attractionDetails);
            SwitchViewButton.Visibility = Visibility.Collapsed;
        }

        private void AttractionDetails_CloseClicked(object sender, EventArgs e)
        {
            if (SwitchViewOnDetailsClose)
            {
                SwitchViewButton.IsChecked = !SwitchViewButton.IsChecked;
            }
            PageMainContent.Children.Remove((AttractionDetails)sender);
            CurrentDetails = null;
            SwitchViewOnDetailsClose = false;
            SwitchViewButton.Visibility=Visibility.Visible;
        }

        private void ClearSearchResults(object sender, EventArgs e)
        {
            SearchBar.Clear();
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            PopulateAttractionsList();
            SetMapMarkers();
            SearchHeader.Visibility = Visibility.Collapsed;
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
            {
                PopulateAttractionsList();
                SetMapMarkers();
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var searchVal = SearchBar.Text;
                List<Attraction> searchResults = Attractions.FindAll(a => a.Name.Contains(searchVal, StringComparison.OrdinalIgnoreCase));
                Attractions = searchResults;
                SearchResultsTitle.Text += "\"" + searchVal + "\"    (" + searchResults.Count + " results)";
                SearchHeader.Visibility = Visibility.Visible;
                PopulateAttractionsList();
                SetMapMarkers();
            }
        }
    }
}
