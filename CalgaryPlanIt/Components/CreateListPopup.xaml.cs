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
    /// Interaction logic for CreateListPopup.xaml
    /// </summary>
    public partial class CreateListPopup : UserControl
    {
        public event EventHandler CloseHandler;
        public CreateListPopup()
        {
            InitializeComponent();
        }

        private void CloseClick(object sender, EventArgs e)
        {
            CloseHandler.Invoke(this, e);
        }

        private void CreateList(object sender, EventArgs e)
        {
            MainWindow.ListofLists.Add(new Lis() { Attractions = new List<Attraction>(), Name = ListName.Text, NumItems=0 });
            CloseHandler.Invoke(this, e);
        }
    }
}
