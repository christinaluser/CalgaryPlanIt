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
    /// Interaction logic for ListAttractionCard.xaml
    /// </summary>
    public partial class ListAttractionCard : UserControl
    {
        public Attraction Attraction;
        public event EventHandler ArchiveButtonClicked;
        public ListAttractionCard()
        {
            InitializeComponent();
        }
        public ListAttractionCard(Attraction attraction)
        {
            InitializeComponent();
            Attraction = attraction;
            SetContent();
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
            AttractionImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Heart.png"));


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ArchiveButtonClicked.Invoke(Attraction, e);
        }
    }
}
