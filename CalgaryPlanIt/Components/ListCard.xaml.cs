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
    /// Interaction logic for ListCard.xaml
    /// </summary>
    public partial class ListCard : UserControl
    {
        public Lis lis;
        public ListCard()
        {
            InitializeComponent();
            this.lis = new Lis();
        }

        public ListCard(Lis lis)
        {
            InitializeComponent();
            this.lis = lis;
            ListName.Text = this.lis.Name;
            if(lis.NumItems > 0)
            {
                NumItems.Text = (lis.Attractions != null ? lis.Attractions.Count.ToString() : 0) + " items";
            }
        }
    }
}
