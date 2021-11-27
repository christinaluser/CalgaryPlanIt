using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Interaction logic for ReviewModal.xaml
    /// </summary>
    public partial class ReviewModal : UserControl
    {
        public Attraction Attraction;
        public bool IsOpen = false;
        public ReviewModal()
        {
            InitializeComponent();
            Attraction = new Attraction();
        }

        public ReviewModal(Attraction attraction)
        {
            InitializeComponent();
            this.Attraction = attraction;
            SetContent();
        }

        private void SetContent()
        {
            Title.Text += Attraction.Name + "?";

        }

        private void StarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            int rating = Int32.Parse(((Button)sender).Tag.ToString());
            if (rating > 0 && rating <= 5)
            {
                Attraction.Rating = rating;
                for (int i = 0; i < 5; i++)
                {
                    if (i < rating)
                    {
                        ((Button)RatingContainer.Children[i]).Content = FindResource("YellowStar" + (i + 1));
                    }
                    else {
                        ((Button)RatingContainer.Children[i]).Content = FindResource("GreyStar" + (i + 1));
                    }
                }
            }
            
        }
    }
}
