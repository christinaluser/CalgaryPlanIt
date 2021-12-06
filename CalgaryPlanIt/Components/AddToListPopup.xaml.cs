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
    /// Interaction logic for AddToListPopup.xaml
    /// </summary>
    public partial class AddToListPopup : UserControl
    {
        Attraction Attraction;
        public AddToListPopup()
        {
            InitializeComponent();

        }

        public AddToListPopup(Attraction attraction)
        {
            InitializeComponent();
            Attraction= attraction;
            SetContent();
        }

        private void SetContent()
        {
            var attcard = new AttractionCard(Attraction);
            attcard.IsHitTestVisible = false;
            Grid.SetRow(attcard, 1);
            MainContent.Children.Add(attcard);

            foreach(Lis lis in MainWindow.ListofLists)
            {
                var grid = new Grid();
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star)});
                grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto)});
                
                var listtb = new TextBlock() { Text=lis.Name, HorizontalAlignment=HorizontalAlignment.Left };
                Grid.SetColumn(listtb, 0);
                grid.Children.Add(listtb);

                var button = new Button() { 
                    Content=new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Images/Heart.png")) },
                    Style= Resources["IconButtonSmallWithBackground"] as Style,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Tag = lis
                };
                Grid.SetColumn(button, 1);
                grid.Children.Add(button);

                var sep = new Separator() { VerticalAlignment=VerticalAlignment.Bottom };
                Grid.SetColumnSpan(sep, 2);
                grid.Children.Add(sep);
            }
        }

        private void CreateAndAddButton_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void CloseClick(object sender, RoutedEventArgs e)
        {

        }


    }
}
