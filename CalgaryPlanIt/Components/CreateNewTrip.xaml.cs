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
        public CreateNewTrip()
        {
            InitializeComponent();
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
            if (String.IsNullOrWhiteSpace(startDateTextBox.Text) | String.IsNullOrWhiteSpace(endDateTextBox.Text) | String.IsNullOrWhiteSpace(TripNameTextBox.Text))
            {

            }
            else
            {
                MainWindow.TripsList.Insert(0, (new Trip()
                {
                    StartDate = DateTime.Parse(startDateTextBox.Text),
                    EndDate = Convert.ToDateTime(endDateTextBox.Text),
                    Name = TripNameTextBox.Text,
                    NumAdults = Convert.ToInt32(AdultCounter.Content),
                    NumChildren = Convert.ToInt32(ChildrenCounter.Content),
                    NumTeens = Convert.ToInt32(TeenCounter.Content),
                }
                ));
                Navigation.HightLightFromStartPlanning();
                AddingNewTripClicked.Invoke(this, e);
            }
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {
            CloseButtonClicked.Invoke(this, e);
        }

    }
}
