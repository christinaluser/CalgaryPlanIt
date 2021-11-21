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
            window.SwitchPage(newPage);
        }
    }
}
