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
        public event EventHandler CloseHandler;
        public AddToListPopup()
        {
            InitializeComponent();

        }

        public AddToListPopup(Attraction attraction)
        {
            InitializeComponent();
            Attraction= attraction;
            SetContent();

            if (Navigation.window.Width <= 450)
            {
                border.Width = 400;
            }
        }

        private void SetContent()
        {
            var attcard = new AttractionCard(Attraction);
            attcard.IsHitTestVisible = false;
            attcard.blur.Opacity = 0;
            attcard.ButtonContainer.Visibility = Visibility.Collapsed;
            Grid.SetRow(attcard, 1);
            MainContent.Children.Add(attcard);

            foreach(Lis lis in MainWindow.ListofLists)
            {
                var grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Stretch;
                grid.Margin = new Thickness(0,0,0,10);

                var c1 = new ColumnDefinition();
                c1.Width = new GridLength(100, GridUnitType.Star);
                grid.ColumnDefinitions.Add(c1);
                var c2 = new ColumnDefinition();
                c2.Width = GridLength.Auto;
                grid.ColumnDefinitions.Add(c2);
                
                var listtb = new TextBlock() { 
                    Text=lis.Name, 
                    HorizontalAlignment=HorizontalAlignment.Left, 
                    VerticalAlignment=VerticalAlignment.Center,
                    Style = Resources["Heading 4"] as Style,
                };
                Grid.SetColumn(listtb, 0);
                grid.Children.Add(listtb);

                var button = new Button() { 
                    Content=new Image() { Source = new BitmapImage(new Uri("pack://application:,,,/Images/Heart.png")) },
                    Style= Resources["IconButtonSmallWithBackground"] as Style,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin=new Thickness(10),
                    Tag = lis
                };
                button.Click += AddToListClicked;
                Grid.SetColumn(button, 1);
                grid.Children.Add(button);

                var sep = new Separator() { VerticalAlignment=VerticalAlignment.Bottom };

                ListsPanel.Children.Add(grid);
                ListsPanel.Children.Add(sep);
            }
        }

        private void CreateAndAddButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.ListofLists.Add(new Lis() { Name = SearchBox.Text, Attractions = new List<Attraction>() { Attraction } });

            CloseHandler.Invoke(this, EventArgs.Empty);
        }

        private void AddToListClicked(object sender, EventArgs e)
        {
            Lis list = ((Button)sender).Tag as Lis;
            var index = MainWindow.ListofLists.FindIndex(l=>l.Name == list.Name);
            if (MainWindow.ListofLists[index].Attractions == null)
            {
                MainWindow.ListofLists[index].Attractions = new List<Attraction> ();
            }
            MainWindow.ListofLists[index].Attractions.Add(Attraction);
            CloseHandler.Invoke(this, EventArgs.Empty);
        }
        
        private void CloseClick(object sender, RoutedEventArgs e)
        {
            CloseHandler.Invoke(this, EventArgs.Empty);
        }


    }
}
