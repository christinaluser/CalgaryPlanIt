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
    /// Interaction logic for AttractionCard.xaml
    /// </summary>
    public partial class AttractionCard : UserControl
    {
        public Attraction Attraction;
        public event EventHandler AttractionCardClicked;
        public event EventHandler AttractionCardAddToListClicked;
        public event EventHandler AttractionCardAddToTripClicked;
        public AttractionCard()
        {
            InitializeComponent();
        }
        public AttractionCard(Attraction attraction)
        {
            InitializeComponent();
            Attraction = attraction;
            SetContent();

            if(Navigation.window.Width <= 450)
            {
                Grid.SetRow(attinfo, 1);
                Grid.SetColumn(attinfo, 0);
                Grid.SetColumnSpan(attinfo, 2);
                attinfo.MaxWidth = 400; 
            }

        }

        private void SetContent()
        {
            AttractionName.Text = Attraction.Name;

            if (Attraction.Rating >= 1)
            {
                Star1.Content = FindResource("YellowStar1");
            } 
            if (Attraction.Rating >= 2)
            {
                Star2.Content = FindResource("YellowStar2");
            } 
            if (Attraction.Rating >= 3)
            {
                Star3.Content = FindResource("YellowStar3");
            } 
            if (Attraction.Rating >= 4)
            {
                Star4.Content = FindResource("YellowStar4");
            }
            if (Attraction.Rating == 5)
            {
                Star5.Content = FindResource("YellowStar5");
            }
            //TODO change to actual attraction image
            if (Attraction.ImageSourceName != null)
            {
                if (Attraction.ImageSourceName.Count > 0)
                {
                    if (Attraction.ImageSourceName.First() != null && Attraction.ImageSourceName.First() != "" && Attraction.ImageSourceName.First() != " ") 
                        AttractionImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Attraction/" + Attraction.ImageSourceName.First()));
                    else
                    {
                        AttractionImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Heart.png"));
                    }
                }
                else
                {
                    AttractionImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Heart.png"));
                }
            }


            var tags = Enum.GetValues(typeof(Tag));
            foreach (Tag tag in tags)
            {
                if (Attraction.Tags.HasFlag(tag) && tag != CalgaryPlanIt.Tag.None)
                {
                    var tagBlock = new TextBlock()
                    {
                        Text = tag.ToFriendlyString(),
                        Padding = new Thickness(10, 5, 10, 5),
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    var border = new Border()
                    {
                        Child = tagBlock,
                        CornerRadius = new CornerRadius(20),
                        Height = 25,
                        Background = Brushes.AliceBlue,
                        BorderBrush = Brushes.AliceBlue,
                        Margin = new Thickness(5)
                    };
                    TagsContainer.Children.Add(border);
                }
            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AttractionCardClicked.Invoke(this, e);
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            CardBorder.BorderThickness = new Thickness(2);
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            CardBorder.BorderThickness = new Thickness(0);
        }

        private void AddToList(object sender, RoutedEventArgs e)
        { 
            AttractionCardAddToListClicked.Invoke(Attraction, e);
        }
            
        private void AddToTrip(object sender, RoutedEventArgs e)
        {
            AttractionCardAddToTripClicked.Invoke(Attraction, e);
        }
    }
}
