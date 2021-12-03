using CalgaryPlanIt.Components;
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

namespace CalgaryPlanIt.Views
{
    /// <summary>
    /// Interaction logic for Lists.xaml
    /// </summary>
    public partial class Lists : Page
    {
        List<Lis> ListofLists;
        public Lists()
        {
            InitializeComponent();
            CreateList();
            AddListCard();
        }

        private void AddListCard()
        {
            ListGrid.Children.Clear();
            foreach(Lis lis in ListofLists)
            {
                ListGrid.Children.Add(new ListCard(lis));
            }

        }

        private void CreateList()
        {
            ListofLists = new List<Lis>();
            ListofLists.Add(new Lis()
            {
                Name = "Resteraunts",
                NumItems = 12
            });
            ListofLists.Add(new Lis()
            {
                Name = "Kids",
                NumItems = 5
            });
            ListofLists.Add(new Lis()
            {
                Name = "Late Night",
                NumItems = 4
            });
        }
    }
}
