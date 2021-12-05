﻿using CalgaryPlanIt.Components;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Plan.xaml
    /// </summary>
    public partial class Plan : Page
    {
        Trip Trip;
        DateTime CurrentPlannerDate;
        public Plan()
        {
            InitializeComponent();
            Trip = new Trip();
        }

        public Plan(Trip trip)
        {
            InitializeComponent();
            Trip = trip;
            CurrentPlannerDate = trip.StartDate;
            SetPageContent();

        }

        private void SetPageContent()
        {
            TripName.Text = Trip.Name;
            TripSummaryCalendar.SelectedDates.AddRange(Trip.StartDate, Trip.EndDate);
            NumTravellers.Text = Trip.GetNumTravellersString();

            PopulateListPanel();
            
            RefreshDay();

        }

        private void RefreshDay()
        {
            PlannerDate.Text = CurrentPlannerDate.ToString("MMMM d, yyyy");

            var DayItineraryItems = Trip.ItineraryItems?.FindAll(i => CurrentPlannerDate.Date.Equals(i.PlannedStartDate.Date));

            ItineraryContainer.Children.Clear();
            ItineraryContainer.ColumnDefinitions.Clear();
            var day = new ItineraryDayScheduler(DayItineraryItems, CurrentPlannerDate, true, false);
            day.ItineraryItemAdded += AddItem;
            day.ItineraryItemRemoved += RemoveItem;
            ItineraryContainer.Children.Add(day);
        }

        private void RefreshWeek()
        {
            // get previous sunday
            if (CurrentPlannerDate.DayOfWeek != DayOfWeek.Sunday)
            {
                int diff = (7 + (CurrentPlannerDate.DayOfWeek - DayOfWeek.Sunday)) % 7;
                CurrentPlannerDate = CurrentPlannerDate.AddDays(-1 * diff).Date;
            }
            DateTime endofweek = CurrentPlannerDate.AddDays(7);
            
            string weekstring = CurrentPlannerDate.ToString("MMMM d") + " ";
            if (endofweek.Year != CurrentPlannerDate.Year)
            {
                weekstring += CurrentPlannerDate.Year.ToString() + " ";
            }
            weekstring += "- ";
            if (endofweek.Month != CurrentPlannerDate.Month)
            {
                weekstring += endofweek.ToString("MMMM") + " ";
            }
            weekstring += endofweek.Day.ToString() + ", " + endofweek.Year.ToString();

            PlannerDate.Text = weekstring;

            ItineraryContainer.Children.Clear();
            ItineraryContainer.ColumnDefinitions.Clear();
            DateTime temp = CurrentPlannerDate;
            for (int i = 0; i < 7; i++)
            {
                ColumnDefinition c1 = new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star)
                };
                if (i == 0)
                {
                    c1.Width = new GridLength(1.35, GridUnitType.Star);
                }
                ItineraryContainer.ColumnDefinitions.Add(c1);
                var DayItineraryItems = Trip.ItineraryItems?.FindAll(i => temp.Date.Equals(i.PlannedStartDate.Date));
                var day = new ItineraryDayScheduler(DayItineraryItems, temp, i == 0, true);
                day.ItineraryItemAdded += AddItem;
                day.ItineraryItemRemoved += RemoveItem;
                Grid.SetColumn(day, i);
                ItineraryContainer.Children.Add(day);
                temp = temp.AddDays(1);
            }

            
        }

        private void AddItem(object sender, EventArgs e)
        {
            var index = MainWindow.TripsList.FindIndex(t => t == Trip || t.Name == Trip.Name);
            Trip.ItineraryItems?.Add((ItineraryItem)sender);
            MainWindow.TripsList[index].ItineraryItems = Trip.ItineraryItems;
        }

        private void RemoveItem(object sender, EventArgs e)
        {
            var index = MainWindow.TripsList.FindIndex(t => t == Trip || t.Name == Trip.Name);
            Trip.ItineraryItems?.Remove((ItineraryItem)sender);
            MainWindow.TripsList[index].ItineraryItems = Trip.ItineraryItems;
        }

        private void ListBackButton_Click(object sender, RoutedEventArgs e)
        {
            ListName.Text = "Lists";
            ListsStackPanel.Children.Clear();
            PopulateListPanel();
            ListBackButton.Visibility= Visibility.Collapsed;
        }

        private void PopulateListPanel()
        {
            foreach (Lis list in MainWindow.ListofLists)
            {
                Button listButton = new Button()
                {
                    Content = list.Name + " (" + (list.Attractions != null ? list.Attractions.Count : 0) + ")",
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Style = Resources["ListButton"] as Style,
                    Tag = list
                };
                listButton.Click += ListButtonClick;
                ListsStackPanel.Children.Add(listButton);
            }
        }

        private void ListButtonClick (object sender, EventArgs e)
        {
            Lis lis = (Lis)((Button)sender).Tag;
            ListName.Text = lis.Name;
            PopulateListAttractions(lis);
            ListBackButton.Visibility = Visibility.Visible;
        }

        private void PopulateListAttractions(Lis list)
        {
            ListsStackPanel.Children.Clear();
            if (list.Attractions != null && list.Attractions.Count > 0)
            {
                foreach (Attraction attraction in list.Attractions)
                {
                    Button listButton = new Button()
                    {
                        Content = new TextBlock() { Text = attraction.Name },
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Background = Brushes.MintCream,
                        Style = Resources["ListButton"] as Style,
                        Tag = attraction,
                        Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream)
                };
                    listButton.PreviewMouseLeftButtonDown += AttractionButtonClick;
                    listButton.PreviewMouseLeftButtonUp += AttractionButtonMouseUp;
                    listButton.GiveFeedback += Button_GiveFeedback;
                    ListsStackPanel.Children.Add(listButton);
                }
            }
        }

        private void AttractionButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Drag.cur")).Stream);
            button.Background = Brushes.AliceBlue;
            DragDrop.DoDragDrop(button,new DataObject(DataFormats.Serializable, button), DragDropEffects.Copy);
            
        }
        
        private void AttractionButtonMouseUp(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var index = ListsStackPanel.Children.IndexOf(button);
            ((Button)ListsStackPanel.Children[index]).Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream);
            
        }

        private void PanelDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Button element)
            {
                element.Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream);
                Attraction attraction = (Attraction)element.Tag;
                Point dropPosition = e.GetPosition(element);
                var textblock = new TextBlock()
                {
                    Text = attraction.Name
                };
                //SchedulePanelItineraryItems.Children.Add(textblock);
            }
        }

        private Cursor customCursor = null;
        private void Button_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (e.Effects == DragDropEffects.Copy)
            {
                if (customCursor == null)
                    customCursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/Drag.cur")).Stream);
                    //customCursor = new Cursor(new FileStream("pack://application:,,,/Views/Drag.cur", FileMode.Open));
                e.UseDefaultCursors = false;
                Mouse.SetCursor(customCursor);
            }
            else
                e.UseDefaultCursors = true;

            e.Handled = true;
        }

        private void NextDayButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlannerDate = CurrentPlannerDate.AddDays(1);
            RefreshDay();
        }

        private void PrevDayButton_Click(object sender, RoutedEventArgs e)
        {
            CurrentPlannerDate = CurrentPlannerDate.AddDays(-1);
            RefreshDay();
        }

        private void DayRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if(MapButton != null) {
                MapButton.Visibility = Visibility.Visible;
                RefreshDay();
            }
                
        }

        private void WeekRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            MapButton.Visibility= Visibility.Hidden;
            RefreshWeek();
        }
    }
}
