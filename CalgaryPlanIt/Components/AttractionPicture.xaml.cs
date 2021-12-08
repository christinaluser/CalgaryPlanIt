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
    /// Interaction logic for AttractionPicture.xaml
    /// </summary>
    public partial class AttractionPicture : UserControl
    {
        public AttractionPicture(String fileName)
        {
            InitializeComponent();
            AttractionImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Attraction/" + fileName));
        }
    }
}
