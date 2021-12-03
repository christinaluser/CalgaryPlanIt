using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Plan.xaml
    /// </summary>
    public partial class Plan : Page
    {
        Trip Trip;
        public Plan()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public Plan(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            SetPageContent();

        }

        private void SetPageContent()
        {
            TripName.Text = Trip.Name;
            TripSummaryCalendar.SelectedDates.AddRange(Trip.StartDate, Trip.EndDate);
            //TripSummaryCalendar.CalendarDayButtonStyle.Setters.Add(new Setter(UIElement.IsHitTestVisibleProperty, false));
            NumTravellers.Text = Trip.GetNumTravellersString();

            //TODO SCHEDULE

            PopulateListPanel();
        }

        private void ListBackButton_Click(object sender, RoutedEventArgs e)
        {
            ListName.Text = "Lists";
            ListsStackPanel.Children.Clear();
            PopulateListPanel();

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

        private void ListButtonClick (object sender, EventArgs e)
        {
            Lis lis = (Lis)((Button)sender).Tag;
            ListName.Text = lis.Name;
            PopulateListAttractions(lis);
            ListBackButton.Visibility = Visibility.Visible;
        }

        private void PopulateListAttractions(Lis list)
        {
            ListsStackPanel.Children.Clear();
            if (list.Attractions != null && list.Attractions.Count > 0)
            {
                foreach (Attraction attraction in list.Attractions)
                {
                    Button listButton = new Button()
                    {
                        Content = attraction.Name,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = Brushes.MintCream,
                        Style = Resources["ListButton"] as Style,
                        Tag = attraction,
                        
                    };
                    listButton.PreviewMouseLeftButtonDown += AttractionButtonClick;
                    listButton.GiveFeedback += Button_GiveFeedback;
                    ListsStackPanel.Children.Add(listButton);
                }
            }
            
        }

        private void AttractionButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            DragDrop.DoDragDrop(button,new DataObject(DataFormats.Serializable, button), DragDropEffects.Copy);
            
        }

        private void PanelDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Button element)
            {
                Attraction attraction = (Attraction)element.Tag;
                Point dropPosition = e.GetPosition(element);
                var textblock = new TextBlock()
                {
                    Text = attraction.Name
                };
                SchedulePanelItineraryItems.Children.Add(textblock);
            }
        }

        private Cursor customCursor = null;
        private void Button_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                if (customCursor == null)
                    customCursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Drag.cur")).Stream);
                    //customCursor = new Cursor(new FileStream("pack://application:,,,/Views/Drag.cur", FileMode.Open));
                e.UseDefaultCursors = false;
                Mouse.SetCursor(customCursor);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }
    }
}
