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

namespace CalgaryPlanIt.Components
{
    /// <summary>
    /// Interaction logic for AddToPlanPopup.xaml
    /// </summary>
    public partial class AddToPlanPopup : UserControl
    {
        //only calgary tower
        ItineraryItem ItineraryItem;
        public event EventHandler CloseClicked;
        public event EventHandler AddToPlanClicked;
        public AddToPlanPopup()
        {
            InitializeComponent();
        }

        public AddToPlanPopup(ItineraryItem item)
        {
            InitializeComponent();
            ItineraryItem = item;
            SetContent();
            if (ItineraryItem?.Name != null && ItineraryItem?.Name != "")
                addbutton.Content = "Save Changes";

            if (Navigation.window.Width <= 450)
            {
                border.Width = 400;
            }
        }

        public void SetContent()
        {
            if (ItineraryItem?.Name != null && ItineraryItem?.Name != "")
            {
                SearchbarGrid.Visibility = Visibility.Collapsed;
                header.Visibility = Visibility.Collapsed;
                Attraction.Children.Clear();
                var attcard = new AttractionCard(MainWindow.AttractionsList.Find(a => a.Name == ItineraryItem.Name));
                attcard.blur.Opacity = 0;
                attcard.IsHitTestVisible = false;
                attcard.CardBorder.Background=Brushes.Transparent;
                attcard.ButtonContainer.Visibility = Visibility.Collapsed;
                Attraction.Children.Add(attcard);
                Attraction.Visibility = Visibility.Visible;
                NotesTextBox.Text = ItineraryItem.Notes;
                
                //CalendarControl.SelectedDate = ItineraryItem.PlannedStartDate;
                //CalendarControl2.SelectedDate = ItineraryItem.PlannedEndDate;
                //endTimeTextBox.Text = ItineraryItem.PlannedEndDate.ToString("t");
                //endDateTextBox.Text = ItineraryItem.PlannedEndDate.ToString("D");
                //startTimeTextBox.Text = ItineraryItem.PlannedStartDate.ToString("t");
                //startDateTextBox.Text = ItineraryItem.PlannedStartDate.ToString("D");

            }
            CalendarControl.SelectedDate = ItineraryItem.PlannedStartDate;
            CalendarControl2.SelectedDate = ItineraryItem.PlannedEndDate;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseClicked.Invoke(this, EventArgs.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ItineraryItem.Notes = NotesTextBox.Text;
            AddToPlanClicked.Invoke(ItineraryItem, EventArgs.Empty);
            CloseClicked.Invoke(this, EventArgs.Empty);
        }

        private void SearchBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                ItineraryItem.Name = SearchBox.Text;
                SetContent();
            }
        }
    }
}
