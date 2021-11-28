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
        Attraction Attraction;
        public AttractionCard()
        {
            InitializeComponent();
        }
        public AttractionCard(Attraction attraction)
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
        }
    }
}
