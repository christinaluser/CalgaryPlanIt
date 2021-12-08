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
    /// Interaction logic for CommentCard.xaml
    /// </summary>
    public partial class CommentCard : UserControl
    {
        Review review;
        public CommentCard(Review review)
        {
            InitializeComponent();
            this.review = review;
            SetContent();
        }

        public void SetContent()
        {
            if (review.Rating >= 1)
            {
                Star1.Content = FindResource("YellowStar1");
            }
            if (review.Rating >= 2)
            {
                Star2.Content = FindResource("YellowStar2");
            }
            if (review.Rating >= 3)
            {
                Star3.Content = FindResource("YellowStar3");
            }
            if (review.Rating >= 4)
            {
                Star4.Content = FindResource("YellowStar4");
            }
            if (review.Rating == 5)
            {
                Star5.Content = FindResource("YellowStar5");
            }
            reviewText.Text = review.Description;
        }
    }

}
