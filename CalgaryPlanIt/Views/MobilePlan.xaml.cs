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
    /// Interaction logic for MobilePlan.xaml
    /// </summary>
    public partial class MobilePlan : Page
    {
        Trip Trip;
        DateTime CurrentPlannerDate;

        Cursor OpenHand = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream);
        Cursor Drag = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Drag.cur")).Stream);
        public MobilePlan()
        {
            InitializeComponent();
        }
        
        public MobilePlan(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            CurrentPlannerDate = trip.StartDate;
            SetPageContent();
            SetMap();
        }

        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            listpanel.Visibility = Visibility.Visible;
        }

        private void ListName_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listpanel.Visibility = Visibility.Collapsed;
        }

        private void SetPageContent()
        {
            TripName.Text = Trip.Name;
            TripSummaryCalendar.SelectedDates.AddRange(Trip.StartDate, Trip.EndDate);
            NumTravellers.Text = Trip.GetNumTravellersString();

            PopulateListPanel();
            RefreshDay();
        }

        private void RefreshDay()
        {
            PlannerDate.Text = CurrentPlannerDate.ToString("MMMM d, yyyy");

            var DayItineraryItems = Trip.ItineraryItems?.FindAll(i => CurrentPlannerDate.Date.Equals(i.PlannedStartDate.Date));

            ItineraryContainer.Children.Clear();
            ItineraryContainer.ColumnDefinitions.Clear();
            var day = new ItineraryDayScheduler(DayItineraryItems, CurrentPlannerDate, true, false);
            day.ItineraryItemAdded += AddItem;
            day.ItineraryItemRemoved += RemoveItem;
            day.BlockClick += HandleBlockClick;
            ItineraryContainer.Children.Add(day);
        }

        private void HandleBlockClick(object sender, EventArgs e)
        {
            ItineraryItem item = (ItineraryItem)sender;

            var atp = new AddToPlanPopup(item);
            atp.CloseClicked += HandleClose;
            atp.AddToPlanClicked += HandleAddToPlan;
            Overlay.Children.Add(atp);
            Overlay.Visibility = Visibility.Visible;

        }

        private void HandleClose(object sender, EventArgs e)
        {
            Overlay.Children.Clear();
            Overlay.Visibility = Visibility.Collapsed;
        }

        private void HandleAddToPlan(object sender, EventArgs e)
        {
            ItineraryItem i = (ItineraryItem)sender;
            if (i?.Name != null && i?.Name != "")
            {
                var index = MainWindow.TripsList.FindIndex(t => t.Name == Trip.Name);
                MainWindow.TripsList[index].ItineraryItems.Add(i);
                Trip.ItineraryItems.Add(i);
                RefreshDay();
            }
        }

        private void AddItem(object sender, EventArgs e)
        {
            var index = MainWindow.TripsList.FindIndex(t => t == Trip || t.Name == Trip.Name);
            Trip.ItineraryItems?.Add((ItineraryItem)sender);
            if (MainWindow.TripsList[index].ItineraryItems == null)
            {
                MainWindow.TripsList[index].ItineraryItems = new List<ItineraryItem>();
            }
            MainWindow.TripsList[index].ItineraryItems.Add((ItineraryItem)sender);
        }

        private void RemoveItem(object sender, EventArgs e)
        {
            var index = MainWindow.TripsList.FindIndex(t => t == Trip || t.Name == Trip.Name);
            Trip.ItineraryItems?.Remove((ItineraryItem)sender);
            MainWindow.TripsList[index].ItineraryItems = Trip.ItineraryItems;
        }

        private void ListBackButton_Click(object sender, RoutedEventArgs e)
        {
            ListName.Text = "Lists";
            ListsStackPanel.Children.Clear();
            PopulateListPanel();
            ListBackButton.Visibility = Visibility.Collapsed;
        }

        private void PopulateListPanel()
        {
            foreach (Lis list in MainWindow.ListofLists)
            {
                Button listButton = new Button()
                {
                    Content = list.Name + " (" + (list.Attractions != null ? list.Attractions.Count : 0) + ")",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Style = Resources["ListButton"] as Style,
                    Tag = list
                };
                listButton.Click += ListButtonClick;
                ListsStackPanel.Children.Add(listButton);
            }
        }

        private void ListButtonClick(object sender, EventArgs e)
        {
            Lis lis = (Lis)((Button)sender).Tag;
            ListName.Text = lis.Name;
            PopulateListAttractions(lis);
            ListBackButton.Visibility = Visibility.Visible;
        }

        private void PopulateListAttractions(Lis list)
        {
            ListsStackPanel.Children.Clear();
            ListsStackPanel.Children.Add(new TextBlock() { Text = "Drag items onto your calendar!" });
            if (list.Attractions != null && list.Attractions.Count > 0)
            {
                foreach (Attraction attraction in list.Attractions)
                {
                    Button listButton = new Button()
                    {
                        Content = new TextBlock() { Text = attraction.Name },
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = Brushes.MintCream,
                        Style = Resources["ListButton"] as Style,
                        Tag = attraction,
                        Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream)
                    };
                    listButton.PreviewMouseLeftButtonDown += AttractionButtonClick;
                    listButton.PreviewMouseLeftButtonUp += AttractionButtonMouseUp;
                    listButton.GiveFeedback += Button_GiveFeedback;
                    ListsStackPanel.Children.Add(listButton);
                }
            }
        }

        private void AttractionButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Cursor = Drag;
            button.Background = Brushes.AliceBlue;
            DragDrop.DoDragDrop(button, new DataObject(DataFormats.Serializable, button), DragDropEffects.Copy);

        }

        private void AttractionButtonMouseUp(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var index = ListsStackPanel.Children.IndexOf(button);
            ((Button)ListsStackPanel.Children[index]).Cursor = OpenHand;

        }

        private void PanelDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Button element)
            {
                element.Cursor = OpenHand;
                Attraction attraction = (Attraction)element.Tag;
                Point dropPosition = e.GetPosition(element);
                var textblock = new TextBlock()
                {
                    Text = attraction.Name
                };
                //SchedulePanelItineraryItems.Children.Add(textblock);
            }
        }

        private Cursor customCursor = null;
        private void Button_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                if (customCursor == null)
                    customCursor = Drag;
                //customCursor = new Cursor(new FileStream("pack://application:,,,/Views/Drag.cur", FileMode.Open));
                e.UseDefaultCursors = false;
                Mouse.SetCursor(customCursor);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

        private void NextDayButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlannerDate = CurrentPlannerDate.AddDays(1);
            RefreshDay();
        }

        private void PrevDayButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlannerDate = CurrentPlannerDate.AddDays(-1);
            RefreshDay();
        }

        private void SwitchViewButton_Click(object sender, RoutedEventArgs e)
        {
            border.Visibility = SwitchViewButton.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private void TripDetailsToggle_Click(object sender, RoutedEventArgs e)
        {
            tripdets.Visibility = TripDetailsToggle.IsChecked == true ? Visibility.Visible : Visibility.Collapsed;
        }

        private Point origin;
        private Point start;
        private void SetMap()
        {
            border.Cursor = OpenHand;
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

        private void MapControlToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void YourLocationToggleButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
