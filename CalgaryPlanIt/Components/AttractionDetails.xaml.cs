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
    /// Interaction logic for AttractionDetails.xaml
    /// </summary>
    public partial class AttractionDetails : UserControl
    {
        Attraction Attraction;
        public event EventHandler AttractionDetailsCloseClicked;
        public AttractionDetails()
        {
            InitializeComponent();
        }

        public AttractionDetails(Attraction attraction)
        {
            InitializeComponent();
            this.Attraction = attraction;
            SetContent();
        }

        private void SetContent()
        {
            Name.Text = Attraction.Name;
            if (Attraction.StartDate != DateTime.MinValue && Attraction.StartDate != null) {
                Dates.Text = Attraction.StartDate?.ToString("MMM dd, yyyy");
                if (Attraction.EndDate != DateTime.MinValue && Attraction.EndDate != null)
                {
                    Dates.Text += " - " + Attraction.EndDate?.ToString("MMM dd, yyyy");
                }
            } else
            {
                Dates.Visibility = Visibility.Collapsed;
            }
            if (Attraction.Duration != null)
                ExpectedDuration.Text += Attraction.Duration.ToString();
            else 
                ExpectedDuration.Visibility = Visibility.Collapsed;
            Address.Text = Attraction.Address;

            Description.Text = Attraction.Description;

            if (Attraction.ImageSourceName != null)
            {
                foreach (String image in Attraction.ImageSourceName) {
                    AttractionPicture attPic = new AttractionPicture(image);
                    PhotoContainer.Children.Add(attPic);
                }
            }

            if (Attraction.Reviews != null)
            {
                foreach (Review review in Attraction.Reviews)
                {
                    CommentCard commentCard = new CommentCard(review);
                    ReviewContainer.Children.Add(commentCard);
                }
            }

            Rating.Text = Attraction.Rating.ToString();
            NumRatings.Text = Attraction.Reviews?.Count.ToString() + " Reviews";

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
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            AttractionDetailsCloseClicked.Invoke(this, new EventArgs());
        }
    }
}
