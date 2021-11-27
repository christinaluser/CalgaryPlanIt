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
        public event EventHandler CloseButtonClicked;
        public Review Review;

        public ReviewModal()
        {
            InitializeComponent();
            Attraction = new Attraction();
        }

        public ReviewModal(Attraction attraction)
        {
            InitializeComponent();
            Attraction = attraction;
            Review = new Review();
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
                Review.Rating = rating;
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

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            CloseButtonClicked.Invoke(this, e);
        }

        private void UploadButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                //TODO https://www.c-sharpcorner.com/UploadFile/mahesh/openfiledialog-in-wpf/
            }
        }

        private void PostButton_Click(object sender, RoutedEventArgs e)
        {
            Review.Description = ReviewTextBox.Text;
            //TODO: should probably have separate handler for post (to demo reviews working)
            CloseButton_Click(sender, e);
        }
    }
}
