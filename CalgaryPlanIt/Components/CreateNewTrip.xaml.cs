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
    /// Interaction logic for CreateNewTrip.xaml
    /// </summary>
    public partial class CreateNewTrip : UserControl
    {
        public event EventHandler CloseButtonClicked;
        public event EventHandler AddingNewTripClicked;
        bool AlreadyValidated = false;
        public CreateNewTrip()
        {
            InitializeComponent();
            if(Navigation.window.Width <= 450)
            {
                numTravellersSelection.Rows = 3;
                numTravellersSelection.Columns = 1;
                StartDate.MinWidth = 0;
                EndDate.MinWidth = 0;
                border.Height += 150;
            }
        }
        private void AdultIncrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(AdultCounter.Content);
            value++;
            string counterValue = value.ToString();
            AdultCounter.Content = counterValue;
        }

        private void AdultDecrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(AdultCounter.Content);
            if (value > 0)
            {
                value--;
                string counterValue = value.ToString();
                AdultCounter.Content = counterValue;
            }
        }

        private void TeenIncrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(TeenCounter.Content);
            value++;
            string counterValue = value.ToString();
            TeenCounter.Content = counterValue;
        }

        private void TeenDecrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(TeenCounter.Content);
            if (value > 0)
            {
                value--;
                string counterValue = value.ToString();
                TeenCounter.Content = counterValue;
            }
        }

        private void ChildrenIncrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(ChildrenCounter.Content);
            value++;
            string counterValue = value.ToString();
            ChildrenCounter.Content = counterValue;
        }

        private void ChildrenDecrease_Click(object sender, RoutedEventArgs e)
        {
            int value = Convert.ToInt32(ChildrenCounter.Content);
            if (value > 0)
            {
                value--;
                string counterValue = value.ToString();
                ChildrenCounter.Content = counterValue;
            }
        }

        public void AddNewTrip_Click(object sender, RoutedEventArgs e)
        {
            if (!AlreadyValidated)
            {
                if (Convert.ToInt32(AdultCounter.Content) == 0 && Convert.ToInt32(ChildrenCounter.Content) == 0 && Convert.ToInt32(TeenCounter.Content) == 0)
                {
                    NumTravellersWarning.Visibility = Visibility.Visible;
                }
                if (StartDate.SelectedDate == null || EndDate.SelectedDate == null)
                {
                    DatesWarning.Text = "No trip dates selected, click 'Start Planning' if you want to continue anyways";
                    DatesWarning.Background = Brushes.NavajoWhite;
                    DatesWarning.Visibility = Visibility.Visible;
                }
                AlreadyValidated = true;
            }
            else
            {
                if (StartDate.SelectedDate > EndDate.SelectedDate)
                {
                    DatesWarning.Text = "End date needs to be after start date";
                    DatesWarning.Background = Brushes.LightCoral;
                    DatesWarning.Visibility = Visibility.Visible;
                }
                else
                {
                    if (String.IsNullOrWhiteSpace(TripNameTextBox.Text))
                    {
                        NameWorning.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MainWindow.TripsList.Insert(0, (new Trip()
                        {
                            StartDate = StartDate.SelectedDate ?? new DateTime(),
                            EndDate = EndDate.SelectedDate ?? new DateTime(),
                            Name = TripNameTextBox.Text,
                            NumAdults = Convert.ToInt32(AdultCounter.Content),
                            NumChildren = Convert.ToInt32(ChildrenCounter.Content),
                            NumTeens = Convert.ToInt32(TeenCounter.Content),
                        }
                        ));
                        AddingNewTripClicked.Invoke(this, e);
                    }
                }
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            CloseButtonClicked.Invoke(this, e);
        }

    }
}
