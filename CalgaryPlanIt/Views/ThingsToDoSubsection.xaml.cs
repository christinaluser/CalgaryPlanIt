using CalgaryPlanIt.Components;
using CalgaryPlanIt.Views;
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

        public ThingsToDoSubsection(Category category)
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
                card.AttractionCardAddToListClicked += AttractionCardAddToList_Clicked;
                AttractionsList.Children.Add(card);
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

        private void OverlayClosed(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            Overlay.Visibility = Visibility.Collapsed;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (Rbutton == "LTH")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return x.Price.CompareTo(y.Price);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "HTL")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Price.CompareTo(x.Price);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "DT")
            {
                Attractions = Attractions.OrderBy(x => x.StartDate).ToList();
                //Attractions.Sort((x, y) => DateTime.Compare((DateTime)x.StartDate, (DateTime)y.StartDate));

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "MP")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Rating.CompareTo(x.Rating);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            if (Sbutton == "S5")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 5)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S4")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 4)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S3")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 3)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S2")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 2)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }

            List<Attraction> TagAttractions = new List<Attraction>();
            if (SelectedTags != CalgaryPlanIt.Tag.None) {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    Tag[] allTags = (Tag[])Enum.GetValues(typeof(Tag));

                    foreach (Tag tag in allTags)
                    {
                        if (SelectedTags.HasFlag(tag) && att.Tags.HasFlag(tag) && !TagAttractions.Contains(att))
                        {
                            TagAttractions.Add(att);

                        }

                    }

                }
                foreach (Attraction att in TagAttractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }

            }
            
             
        }

        private void LTH_Checked(object sender, RoutedEventArgs e)
        {   RadioButton li = sender as RadioButton;
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Attractions = TmpAttractions;
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                AttractionsList.Children.Add(new AttractionCard(attraction));
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
            Trip trip = (Trip)sender;
            //TODO set filters
        }
    }
}
