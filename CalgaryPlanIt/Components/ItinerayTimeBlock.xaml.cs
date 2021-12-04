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
    /// Interaction logic for ItinerayTimeBlock.xaml
    /// </summary>
    public partial class ItinerayTimeBlock : UserControl
    {
        ItineraryItem Item = new ItineraryItem();
        public DateTime StartOfTimeBlock;

        public ItinerayTimeBlock()
        {
            InitializeComponent();
            HideContents();

        }
        
        public ItinerayTimeBlock(DateTime t)
        {
            InitializeComponent();
            HideContents();
            StartOfTimeBlock = t;
        }

        public ItinerayTimeBlock(ItineraryItem item)
        {
            InitializeComponent();
            Item = item;
        }

        public void MakeVisible()
        {

            TimeBlockComponent.Background = new SolidColorBrush(Color.FromArgb(90, 255, 192, 203));
            AttractionName.Visibility = Visibility.Visible;
            AttractionName.Text = Item.Name;
            Address.Visibility = Visibility.Visible;
            Address.Text = Item.Address;
            Time.Visibility = Visibility.Visible;
            Time.Text = Item.GetTimeString();
        }

        public void HideContents()
        {
            //TimeBlockComponent.Visibility = Visibility.Hidden;
            RemoveButton.Visibility = Visibility.Hidden;
            TimeBlockComponent.Background = Brushes.Transparent;
            AttractionName.Visibility = Visibility.Hidden;
            Address.Visibility = Visibility.Hidden;
            Time.Visibility = Visibility.Hidden;
        }

        private void PanelDrop(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);
            if (data is Button element)
            {
                element.Cursor = new Cursor(Application.GetResourceStream(new Uri("pack://application:,,,/OpenHand.cur")).Stream);
                element.Background = Brushes.MintCream;
                Attraction attraction = (Attraction)element.Tag;
                Item = new ItineraryItem(attraction);
                Item.PlannedStartDate = StartOfTimeBlock;
                Item.PlannedEndDate = StartOfTimeBlock.AddHours(1);
                MakeVisible();
            }
        }

        private void OnDragEnter(object sender, EventArgs e)
        {
            TimeBlockComponent.BorderBrush = Brushes.LightBlue;
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            TimeBlockComponent.BorderBrush = Brushes.Transparent;
        }
    }
}
