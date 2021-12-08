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
    /// Interaction logic for MobileThingsToDoSubsection.xaml
    /// </summary>
    public partial class MobileThingsToDoSubsection : Page
    {
        Category Category;
        string Rbutton;
        string Sbutton;
        Tag SelectedTags = CalgaryPlanIt.Tag.None;
        List<Attraction> Attractions = new List<Attraction>();
        List<Attraction> TagAttractions = new List<Attraction>();
        List<Attraction> TmpAttractions = new List<Attraction>();
        AttractionDetails? CurrentDetails;
        bool SwitchViewOnDetailsClose = false;


        private Point origin;
        private Point start;
        public MobileThingsToDoSubsection()
        {
            InitializeComponent();
        }
        public MobileThingsToDoSubsection(Category category)
        {
            InitializeComponent();
            Category = category;
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            TmpAttractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
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
                if (tag != CalgaryPlanIt.Tag.None)
                {
                    CheckBox checkbox = new CheckBox()
                    {
                        Content = tag.ToFriendlyString(),
                        Tag = tag
                    };
                    this.RegisterName(tag.ToString(), checkbox);
                    checkbox.Checked += new RoutedEventHandler(AddFilterTag);
                    checkbox.Unchecked += new RoutedEventHandler(RemoveFilterTag);
                    FilterTagsPanel.Children.Add(checkbox);
                }
            }
        }

        private void PopulateAttractionsList()
        {
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                var card = new AttractionCard(attraction);
                card.AttractionCardClicked += AttractionCard_Clicked;
                card.AttractionCardAddToListClicked += AttractionCardAddToList_Clicked;
                card.AttractionCardAddToTripClicked += AttractionCardAddToTrip_Clicked;
                AttractionsList.Children.Add(card);
            }
            if (AttractionsList.Children.Count == 0)
            {
                AttractionsList.Children.Add(new TextBlock() { Text = "No attractions here :(", HorizontalAlignment = HorizontalAlignment.Center });
            }
        }

        private void AttractionCardAddToList_Clicked(object sender, EventArgs e)
        {
            Attraction att = (Attraction)sender;
            var overlay = new AddToListPopup(att) { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            overlay.CloseHandler += OverlayClosed;
            Overlay.Children.Add(overlay);
            Overlay.Visibility = Visibility.Visible;
        }

        private Attraction att;
        private Trip t;
        private void AttractionCardAddToTrip_Clicked(object sender, EventArgs e)
        {
            att = (Attraction)sender;
            var overlay = new TripPickerPopup() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            overlay.CloseHandler += OverlayClosed;
            overlay.TripSelectedHandler += AttractionCardAddToTripNext_Clicked;
            Overlay.Children.Add(overlay);
            Overlay.Visibility = Visibility.Visible;

        }

        private void AttractionCardAddToTripNext_Clicked(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            t = (Trip)sender;
            ItineraryItem item = new ItineraryItem(att);
            item.PlannedStartDate = new DateTime(t.StartDate.Year, t.StartDate.Month, t.StartDate.Day, t.StartDate.Hour, 0, 0);
            item.PlannedEndDate = new DateTime(t.StartDate.Year, t.StartDate.Month, t.StartDate.Day, t.StartDate.Hour + 1, 0, 0);
            var overlay = new AddToPlanPopup(item) { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            overlay.CloseClicked += OverlayClosed;
            overlay.AddToPlanClicked += AttractionCardAddToTripFinal_Clicked;
            Overlay.Children.Add(overlay);
            Overlay.Visibility = Visibility.Visible;
        }

        private void AttractionCardAddToTripFinal_Clicked(object sender, EventArgs e)
        {
            ItineraryItem i = (ItineraryItem)sender;
            if (i?.Name != null && i?.Name != "")
            {
                var index = MainWindow.TripsList.FindIndex(trip => trip == t || trip.Name == t.Name);
                if (MainWindow.TripsList[index].ItineraryItems == null)
                    MainWindow.TripsList[index].ItineraryItems = new List<ItineraryItem> { i };
                else
                    MainWindow.TripsList[index].ItineraryItems.Add(i);
            }
        }

        private void OverlayClosed(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            Overlay.Visibility = Visibility.Collapsed;
        }

        public void AddFilterTag(object sender, RoutedEventArgs e)
        {
            Tag t = (Tag)((CheckBox)sender).Tag;
            SelectedTags |= t;
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


        //Apply filters
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //sort
            if (Rbutton == "LTH")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return x.Price.CompareTo(y.Price);
                });

                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    AttractionsList.Children.Add(new AttractionCard(att));
                //}
            }
            else if (Rbutton == "HTL")
            {
                Attractions = Attractions.OrderByDescending(x => x.Price).ToList();
                /*
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Price.CompareTo(x.Price);
                });
                */
                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    AttractionsList.Children.Add(new AttractionCard(att));
                //}
            }
            else if (Rbutton == "DT")
            {
                Attractions = Attractions.OrderBy(x => x.StartDate).ToList();
                //Attractions.Sort((x, y) => DateTime.Compare((DateTime)x.StartDate, (DateTime)y.StartDate));

                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    AttractionsList.Children.Add(new AttractionCard(att));
                //}
            }
            else if (Rbutton == "MP")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Rating.CompareTo(x.Rating);
                });

                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    AttractionsList.Children.Add(new AttractionCard(att));
                //}
            }

            //filter rating
            if (Sbutton == "S5")
            {
                Attractions = Attractions.FindAll(a => a.Rating == 5);
                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    if (att.Rating == 5)
                //    {
                //        AttractionsList.Children.Add(new AttractionCard(att));
                //    }
                //}
            }
            else if (Sbutton == "S4")
            {
                Attractions = Attractions.FindAll(a => a.Rating >= 4);
                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    if (att.Rating == 4)
                //    {
                //        AttractionsList.Children.Add(new AttractionCard(att));
                //    }
                //}
            }
            else if (Sbutton == "S3")
            {
                Attractions = Attractions.FindAll(a => a.Rating >= 3);
                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    if (att.Rating == 3)
                //    {
                //        AttractionsList.Children.Add(new AttractionCard(att));
                //    }
                //}
            }
            else if (Sbutton == "S2")
            {
                Attractions = Attractions.FindAll(a => a.Rating >= 2);
                //AttractionsList.Children.Clear();
                //foreach (Attraction att in Attractions)
                //{
                //    if (att.Rating == 2)
                //    {
                //        AttractionsList.Children.Add(new AttractionCard(att));
                //    }
                //}
            }

            List<Attraction> TagAttractions = new List<Attraction>();
            if (SelectedTags != CalgaryPlanIt.Tag.None)
            {
                foreach (Attraction att in Attractions)
                {
                    Tag[] allTags = (Tag[])Enum.GetValues(typeof(Tag));
                    foreach (Tag tag in allTags)
                    {
                        if (tag != CalgaryPlanIt.Tag.None && SelectedTags.HasFlag(tag) && att.Tags.HasFlag(tag))
                        {
                            TagAttractions.Add(att);
                            break;
                        }
                    }
                }
                Attractions = TagAttractions;
            }

            if (FilterStartDate.SelectedDate != null && FilterEndDate.SelectedDate != null)
            {
                List<Attraction> datefiltered = new List<Attraction>();

                foreach (Attraction att in Attractions)
                {
                    if (att.StartDate >= FilterStartDate.SelectedDate && att.StartDate <= FilterEndDate.SelectedDate)
                    {
                        datefiltered.Add(att);
                    }
                }

                Attractions = datefiltered;
            }


            PopulateAttractionsList();
            filtersv.Visibility = Visibility.Collapsed;

        }

        private void LTH_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();

        }

        private void HTL_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void MP_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void NY_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void DT_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void S5_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();

        }

        private void S4_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        private void S3_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        private void S2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        //clear filters
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Attractions = TmpAttractions;
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                AttractionsList.Children.Add(new AttractionCard(attraction));
            }
            Button_Click_2(sender, e);
        }

        private void SwitchViewButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AttractionCard_Clicked(object sender, EventArgs e)
        {
            var attraction = ((AttractionCard)sender).Attraction;
            var attractionDetails = new AttractionDetails(attraction);

            attractionDetails.AttractionDetailsCloseClicked += AttractionDetails_CloseClicked;
            CurrentDetails = attractionDetails;
            ForDetails.Children.Add(attractionDetails);
            ForDetails.Visibility = Visibility.Visible;
        }

        private void AttractionDetails_CloseClicked(object sender, EventArgs e)
        {

            ForDetails.Children.Remove((AttractionDetails)sender);
            ForDetails.Visibility = Visibility.Collapsed;
            CurrentDetails = null;
        }

        private void ClearSearchResults(object sender, EventArgs e)
        {
            SearchBar.Clear();
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            PopulateAttractionsList();
            SetMapMarkers();
            SearchHeader.Visibility = Visibility.Collapsed;
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

        private void AutoApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var overlay = new TripPickerPopup() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            overlay.CloseHandler += OverlayClosed;
            overlay.TripSelectedHandler += FilterByTrip;
            Overlay.Children.Add(overlay);
            Overlay.Visibility = Visibility.Visible;
        }

        private void FilterByTrip(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            Overlay.Visibility = Visibility.Collapsed;
            Trip trip = (Trip)sender;
            if (trip != null)
            {
                if (trip.NumChildren > 0)
                {
                    CheckBox cb = (CheckBox)this.FindName(CalgaryPlanIt.Tag.KidFriendly.ToString());
                    cb.IsChecked = true;
                }
                if (trip.NumTeens > 0)
                {
                    CheckBox cb = (CheckBox)this.FindName(CalgaryPlanIt.Tag.TeenFriendly.ToString());
                    cb.IsChecked = true;
                }
                if (trip.NumChildren == 0 && trip.NumTeens == 0 && trip.NumAdults > 0)
                {
                    CheckBox cb = (CheckBox)this.FindName(CalgaryPlanIt.Tag.AdultOnly.ToString());
                    cb.IsChecked = true;
                }

                FilterStartDate.SelectedDate = trip.StartDate;
                FilterEndDate.SelectedDate = trip.EndDate;

                Button_Click(sender, null);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            FilterStartDate.SelectedDate = null;
            FilterEndDate.SelectedDate = null;
        }

        private void FilterToggle_Click(object sender, RoutedEventArgs e)
        {
            if (FilterToggle.IsChecked == true)
            {
                filtersv.Visibility = Visibility.Visible;
            }
            else
            {
                filtersv.Visibility = Visibility.Collapsed;
            }
        }

        private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            if (MapButton.IsChecked == true)
            {
                border.Visibility = Visibility.Visible;
                AttractionsList.Orientation = Orientation.Horizontal;
                mainsv.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                mainsv.Height = 225;
                mainsv.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            }
            else
            {
                border.Visibility = Visibility.Collapsed;
                AttractionsList.Orientation = Orientation.Vertical;
                mainsv.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                mainsv.Height = 660;
                mainsv.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            }
        }
    }
}

