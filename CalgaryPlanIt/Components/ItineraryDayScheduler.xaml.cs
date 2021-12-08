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
        bool ShowLabel;
        bool ShowDayOfWeek;
        public event EventHandler ItineraryItemAdded;
        public event EventHandler ItineraryItemRemoved;
        public event EventHandler BlockClick;


        public ItineraryDayScheduler()
        {
            InitializeComponent();
            ItineraryItems = new List<ItineraryItem>();
            InitDay();
        }

        public ItineraryDayScheduler(List<ItineraryItem> items, DateTime date, bool showLabel, bool showDayOfWeek)
        {
            InitializeComponent();
            ItineraryItems = items;
            ShowDayOfWeek = showDayOfWeek;
            ShowLabel = showLabel;
            Date = date;
            InitDay();
        }

        public void InitDay()
        {
            int gridRow = 0;
            DateTime currentDayMidnight = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0);

            if (ShowDayOfWeek)
            {
                DayOfWeekLabel.Text = currentDayMidnight.ToString("ddd");
                DayLabel.Text = currentDayMidnight.Day.ToString();
                DayLabel.Visibility = Visibility.Visible;
                DayOfWeekLabel.Visibility = Visibility.Visible;
                if (ShowLabel)
                {
                    DayOfWeekLabel.Margin = new Thickness(25, 0, 0, 0);
                    DayLabel.Margin = new Thickness(25, 0, 0, 0);
                }
            } else
            {
                DayLabel.Visibility = Visibility.Collapsed;
                DayOfWeekLabel.Visibility = Visibility.Collapsed;
            }

            for (int i = 0; i < 25; i++)
            {
                DateTime timeblock = currentDayMidnight.AddHours(i);
                if (i != 24)
                {
                    var label = new TextBlock()
                    {
                        Text = timeblock.ToString("h tt"),
                        Style = Resources["Heading 5"] as Style,
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Margin = new Thickness(0, 0, 0, 2)
                    };
                    Grid.SetRow(label, gridRow);
                    
                    if (!ShowLabel){ 
                        label.Visibility = Visibility.Hidden;
                    }

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
                    ItineraryItem? item = ItineraryItems?.Find(it => it.PlannedStartDate.Hour == (timeblock.Hour - 1));

                    if (item == null)
                    {
                        var tbcomponent = new ItinerayTimeBlock(timeblock.AddHours(-1))
                        {
                            Margin = new Thickness(40, 0, 0, 0),
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        if (!ShowLabel)
                        {
                            tbcomponent.Margin = new Thickness(0, 0, 0, 0);
                            tbcomponent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        Grid.SetRow(tbcomponent, gridRow);
                        TimeBlockGrid.Children.Add(tbcomponent);
                        tbcomponent.ItineraryItemAdded += AddItem;
                        tbcomponent.ItineraryItemRemoved += RemoveItem;
                        tbcomponent.BlockClick += HandleBlockClick;
                    }
                    else
                    {
                        var tbcomponent = new ItinerayTimeBlock(timeblock.AddHours(-1), item)
                        {
                            Margin = new Thickness(40, 0, 0, 0),
                            VerticalAlignment = VerticalAlignment.Top
                        };
                        if (!ShowLabel)
                        {
                            tbcomponent.Margin = new Thickness(0, 0, 0, 0);
                            tbcomponent.HorizontalAlignment = HorizontalAlignment.Stretch;
                        }
                        tbcomponent.ItineraryItemAdded += AddItem;
                        tbcomponent.ItineraryItemRemoved += RemoveItem;
                        tbcomponent.BlockClick += HandleBlockClick;
                        Grid.SetRow(tbcomponent, gridRow);
                        TimeBlockGrid.Children.Add(tbcomponent);
                    }

                }
                gridRow++;
            }
        }

        private void AddItem(object sender, EventArgs e)
        {
            if (ItineraryItems == null)
            {
                ItineraryItems = new List<ItineraryItem>(); 
            }
            ItineraryItems.Add((ItineraryItem)sender);
            ItineraryItemAdded.Invoke(sender, e);
        }

        private void RemoveItem(object sender, EventArgs e)
        {
            ItineraryItemRemoved.Invoke(sender, e);
            ItineraryItems.Remove((ItineraryItem)sender);
        }

        private void HandleBlockClick(object sender, EventArgs e)
        {
            BlockClick.Invoke(sender, e);
        }
    }
}
