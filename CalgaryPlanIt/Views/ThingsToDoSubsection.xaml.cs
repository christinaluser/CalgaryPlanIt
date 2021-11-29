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

            var mapMarker = new MapMarker("You", null);
            Canvas.SetTop(mapMarker, 100);
            Canvas.SetLeft(mapMarker, 100);
            MapCanvas.Children.Add(mapMarker);

            var mapMarker2 = new MapMarker(Attractions[0].Name, null, true);
            Canvas.SetTop(mapMarker2, 200);
            Canvas.SetLeft(mapMarker2, 200);
            MapCanvas.Children.Add(mapMarker2);


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
                AttractionsList.Children.Add(new AttractionCard(attraction));
            }
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

        private void SwitchViewButton_Click(object sender, RoutedEventArgs e)
        {
            
        }


    }
}
