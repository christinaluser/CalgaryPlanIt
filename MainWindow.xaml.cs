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
            Navigation.window = this;
            Navigation.NavigateTo(new Home());
            
        }

        public void SwitchPage(Page newPage)
        {
            Main.Content = newPage;
        }
    }
}
