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
    /// Interaction logic for ItineraryDayList.xaml
    /// </summary>
    public partial class ItineraryDayList : UserControl
    {
        public DateTime Day;
        public List<ItineraryItem> ItineraryItems;
        public bool ShowReviewButtons;
        public event EventHandler ReviewButtonClicked;
 
        public ItineraryDayList()
        {
            InitializeComponent();
        }

        public ItineraryDayList(DateTime day, List<ItineraryItem> itineraryItems, bool showReviewButtons)
        {
            InitializeComponent();
            Day = day;
            ItineraryItems = itineraryItems.OrderBy(i=>i.PlannedStartDate).ToList();
            ShowReviewButtons = showReviewButtons;

            SetContent();
        }

        private void SetContent()
        {
            int row = 0;
            Date.Text = Day.ToString("dddd MMMM dd");
            if (ItineraryItems.Count == 0)
            {
                ItineraryItemsGrid.RowDefinitions.Add(new RowDefinition());
                TextBlock timetextBlock = new()
                {
                    Text = "Nothing planned yet",
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(0, 10, 0, 0),
                };
                Grid.SetRow(timetextBlock, row);
                ItineraryItemsGrid.Children.Add(timetextBlock);
            }
            else
            {
                if (Navigation.window.Width > 450)
                {

                    foreach (ItineraryItem itineraryItem in ItineraryItems)
                    {
                        ItineraryItemsGrid.RowDefinitions.Add(new RowDefinition());
                        TextBlock timetextBlock = new()
                        {
                            Text = itineraryItem.PlannedStartDate.ToString("h tt") + " - " + itineraryItem.PlannedEndDate.ToString("h tt"),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(0, 10, 0, 0),
                        };
                        Grid.SetRow(timetextBlock, row);
                        ItineraryItemsGrid.Children.Add(timetextBlock);

                        TextBlock textBlock = new()
                        {
                            Text = itineraryItem.Name + "\n" + itineraryItem.Address,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center,
                            Padding = new Thickness(10)
                        };
                        Grid.SetRow(textBlock, row);
                        ItineraryItemsGrid.Children.Add(textBlock);

                        Separator sep = new Separator() { VerticalAlignment = VerticalAlignment.Bottom };
                        Grid.SetRow(sep, row);
                        ItineraryItemsGrid.Children.Add(sep);

                        if (ShowReviewButtons)
                        {
                            Button reviewButton = new Button()
                            {
                                Content = "Review",
                                Tag = itineraryItem,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Center,
                                Background = Brushes.LightBlue,
                                Padding = new Thickness(10),
                                Margin = new Thickness(0, 0, 0, 10)
                            };
                            reviewButton.Click += new RoutedEventHandler(HandleReviewButton_Click);
                            Grid.SetRow(reviewButton, row);
                            ItineraryItemsGrid.Children.Add(reviewButton);
                        }

                        row++;
                    }
                }
                else
                {
                    foreach (ItineraryItem itineraryItem in ItineraryItems)
                    {
                        ItineraryItemsGrid.RowDefinitions.Add(new RowDefinition());
                        TextBlock timetextBlock = new()
                        {
                            Text = itineraryItem.PlannedStartDate.ToString("h tt") + " - " + itineraryItem.PlannedEndDate.ToString("h tt"),
                            HorizontalAlignment = HorizontalAlignment.Left,
                            Margin = new Thickness(0, 10, 0, 0),
                        };
                        //Grid.SetRow(timetextBlock, row);
                        //ItineraryItemsGrid.Children.Add(timetextBlock);

                        TextBlock textBlock = new()
                        {
                            Text = itineraryItem.Name + "\n" + itineraryItem.Address,
                            TextWrapping = TextWrapping.Wrap,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            TextAlignment = TextAlignment.Center,
                            Padding = new Thickness(10)
                        };
                        //Grid.SetRow(textBlock, row);
                        //ItineraryItemsGrid.Children.Add(textBlock);

                        Separator sep = new Separator() { VerticalAlignment = VerticalAlignment.Bottom };
                        Grid.SetRow(sep, row);
                        ItineraryItemsGrid.Children.Add(sep);

                        StackPanel sp = new StackPanel();
                        sp.Children.Add(timetextBlock);
                        sp.Children.Add(textBlock);
                        

                        if (ShowReviewButtons)
                        {
                            Button reviewButton = new Button()
                            {
                                Content = "Review",
                                Tag = itineraryItem,
                                HorizontalAlignment = HorizontalAlignment.Right,
                                VerticalAlignment = VerticalAlignment.Center,
                                Background = Brushes.LightBlue,
                                Padding = new Thickness(10),
                                Margin = new Thickness(0, 0, 0, 10)
                            };
                            reviewButton.Click += new RoutedEventHandler(HandleReviewButton_Click);
                            //Grid.SetRow(reviewButton, row);
                            //ItineraryItemsGrid.Children.Add(reviewButton);
                            sp.Children.Add(reviewButton);
                        }
                        Grid.SetRow(sp, row);
                        ItineraryItemsGrid.Children.Add(sp);
                        row++;
                    }
                }
            }
            
            
        }

        private void HandleReviewButton_Click(object sender, RoutedEventArgs e)
        {
            ReviewButtonClicked.Invoke(sender, e);
        }
    }
}
