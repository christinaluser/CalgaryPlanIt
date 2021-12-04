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
    /// Interaction logic for ItineraryDayScheduler.xaml
    /// </summary>
    public partial class ItineraryDayScheduler : UserControl
    {
        DateTime Date;
        List<ItineraryItem> ItineraryItems;
        public ItineraryDayScheduler()
        {
            InitializeComponent();
            PopulateDay();
        }

        public ItineraryDayScheduler(List<ItineraryItem> items, DateTime date)
        {
            InitializeComponent();
            ItineraryItems = items;
            Date = date;
            PopulateDay();
        }

        public void PopulateDay()
        {
            int gridRow = 0;
            DateTime currentDayMidnight = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);

            gridRow++;
            for (int i = 0; i < 25; i++)
            {
                DateTime timeblock = currentDayMidnight.AddHours(i);
                if (i != 25)
                {
                    var label = new TextBlock()
                    {
                        Text = timeblock.ToString("h tt"),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Margin = new Thickness(0, 0, 0, 2)
                    };
                    Grid.SetRow(label, gridRow);
                    TimeBlockGrid.Children.Add(label);

                    var sep = new Separator()
                    {
                        VerticalAlignment = VerticalAlignment.Bottom
                    };
                    Grid.SetRow(sep, gridRow);
                    TimeBlockGrid.Children.Add(sep);

                }
                
                if (i != 0)
                {
                    var tbcomponent = new ItinerayTimeBlock(timeblock.AddHours(-1))
                    {
                        Margin = new Thickness(40, 0, 0, 0),
                        VerticalAlignment = VerticalAlignment.Top
                    };
                    Grid.SetRow(tbcomponent, gridRow);
                    TimeBlockGrid.Children.Add(tbcomponent);
                }
                

                gridRow++;
            }

            
        }


    }
}
