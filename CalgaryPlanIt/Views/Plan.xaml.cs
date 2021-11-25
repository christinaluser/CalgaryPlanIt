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
        }

        private void HandleEditDates_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
