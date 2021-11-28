using CalgaryPlanIt.Components;
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

namespace CalgaryPlanIt.Views
{
    /// <summary>
    /// Interaction logic for ThingsToDoSubsection.xaml
    /// </summary>
    public partial class ThingsToDoSubsection : Page
    {
        Category Category;
        Tag SelectedTags = CalgaryPlanIt.Tag.None;
        List<Attraction> Attractions = new List<Attraction>();
        public ThingsToDoSubsection(Category category)
        {
            InitializeComponent();
            Category = category;
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            SetContent();
        }

        public void SetContent()
        {
            CategoryName.Text = Category.ToFriendlyString();
            PopulateFilterTags();
            PopulateAttractionsList();
        }

        private void PopulateFilterTags()
        {
            FilterTagsPanel.Children.Clear();
            Tag[] allTags = (Tag[])Enum.GetValues(typeof(Tag));
            foreach (Tag tag in allTags)
            {
                CheckBox checkbox = new CheckBox() { 
                    Content = tag.ToFriendlyString(),
                    Tag = tag
                };
                checkbox.Checked += new RoutedEventHandler(AddFilterTag);
                checkbox.Unchecked += new RoutedEventHandler(RemoveFilterTag);
                FilterTagsPanel.Children.Add(checkbox);
            }
        }

        private void PopulateAttractionsList()
        {
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                AttractionsList.Children.Add(new AttractionCard(attraction));
            }
        }

        public void AddFilterTag(object sender, RoutedEventArgs e)
        {
            Tag t = (Tag)((CheckBox)sender).Tag;
            SelectedTags |= t;
        }

        public void RemoveFilterTag(object sender, RoutedEventArgs e)
        {
            Tag t = (Tag)((CheckBox)sender).Tag;
            var mask = ~t;
            SelectedTags &= mask;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NavigateTo(new ThingsToDo());
        }
    }
}
