using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CalgaryPlanIt
{
    public static class Navigation
    {
        public static MainWindow window;

        public static void NavigateTo(Page newPage)
        {
            window.navbar.Visibility = System.Windows.Visibility.Visible;
            window.mobilenavbar.Visibility = System.Windows.Visibility.Collapsed;
            window.Width = 1500;
            window.SwitchPage(newPage);
        }
        
        public static void HightLightFromStartPlanning ()
        {
            window.navbar.HighlightNavBarButton("Things To Do");
        }

        public static void NavigateToMobile(Page newPage)
        {
            window.navbar.Visibility = System.Windows.Visibility.Collapsed;
            window.mobilenavbar.Visibility = System.Windows.Visibility.Visible;
            window.Width = 450;

            window.SwitchPage(newPage);
            window.mobilenavbar.NavBarButtonGroup.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
