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
        readonly Home _home = new Home();
        readonly ThingsToDo _thingsToDo = new ThingsToDo();
        readonly Trips _trips = new Trips();
        readonly Lists _lists = new Lists();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Children.Clear();
            PageContent.Children.Add(_home);
        }

        private void ThingsToDoButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Children.Clear();
            PageContent.Children.Add(_thingsToDo);
        }

        private void TripsButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Children.Clear();
            PageContent.Children.Add(_trips);
        }

        private void ListsButton_Click(object sender, RoutedEventArgs e)
        {
            PageContent.Children.Clear();
            PageContent.Children.Add(_lists);
        }
    }
}
