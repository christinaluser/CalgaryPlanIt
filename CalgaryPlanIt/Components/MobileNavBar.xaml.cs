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

namespace CalgaryPlanIt.Components
{
    /// <summary>
    /// Interaction logic for MobileNavBar.xaml
    /// </summary>
    public partial class MobileNavBar : UserControl
    {
        public MobileNavBar()
        {
            InitializeComponent();
        }
        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateToMobile(new HomeCurrentTrip());
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void ThingsToDoButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateToMobile(new ThingsToDo(true));
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void TripsButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateToMobile(new Trips(true));
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateToMobile(new Lists(true));
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }
        

        public void HighlightNavBarButton(string buttonName)
        {
            foreach (var button in NavBarButtonGroup.Children)
            {
                if (button.GetType() == typeof(Button))
                {
                    if (((Button)button).Content.ToString() == buttonName)
                    {
                        ((Button)button).Background = Brushes.Beige;
                    }
                    else
                    {
                        ((Button)button).Background = Brushes.Transparent;
                    }
                }
            }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Navigation.NavigateTo(new Home());
        }

        private void ToggleButton_Checked(object sender, RoutedEventArgs e)
        {
            NavBarButtonGroup.Visibility = Visibility.Visible;
        }

        private void ToggleButton_Unchecked(object sender, RoutedEventArgs e)
        {
            NavBarButtonGroup.Visibility = Visibility.Collapsed;
        }
    }
}
