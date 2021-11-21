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

namespace CalgaryPlanIt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Home _home = new Home();
        ThingsToDo _thingsToDo = new ThingsToDo();
        Trips _trips = new Trips();
        Lists _lists = new Lists();

        public MainWindow()
        {
            InitializeComponent();
            Main.Content = _home;
            
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = _home;
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void ThingsToDoButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = _thingsToDo;
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void TripsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = _trips;
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = _lists;
            HighlightNavBarButton(((Button)sender).Content.ToString());
        }

        private void HighlightNavBarButton(string buttonName)
        {
            foreach(var button in NavBar.Children)
            {
                if(button.GetType() == typeof(Button))
                {
                    if (((Button)button).Content == buttonName)
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
    }
}
