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
    /// Interaction logic for ThingsToDo.xaml
    /// </summary>
    public partial class ThingsToDo : Page
    {
        List<KeyValuePair<string,Category>> allCategories = new List<KeyValuePair<string, Category>>();
        List<KeyValuePair<string,Category>> allCategoriesCopy = new List<KeyValuePair<string, Category>>();
        string SortType = "Recommended";
        public ThingsToDo()
        {
            InitializeComponent();
            var allCategoriesArr = (Category[])Enum.GetValues(typeof(Category));
            foreach (var cat in allCategoriesArr)
            {
                KeyValuePair<string, Category> keyValuePair = new KeyValuePair<string, Category>(cat.ToString(), cat);
                allCategories.Add(keyValuePair);
                allCategoriesCopy.Add(keyValuePair);
            }
            AddCategoryCards();

        }
        public ThingsToDo(bool isMobile)
        {
            InitializeComponent();

            Grid.SetColumnSpan(tb, 2);
            Grid.SetRow(sb, 1);
            Grid.SetColumn(sb, 1);
            sb.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetColumn(cb, 0);
            Grid.SetRow(cb, 1);
            cb.HorizontalAlignment = HorizontalAlignment.Left;

            Grid.SetRow(sep, 1);

            var allCategoriesArr = (Category[])Enum.GetValues(typeof(Category));
            foreach (var cat in allCategoriesArr)
            {
                KeyValuePair<string, Category> keyValuePair = new KeyValuePair<string, Category>(cat.ToString(), cat);
                allCategories.Add(keyValuePair);
                allCategoriesCopy.Add(keyValuePair);
            }
            AddCategoryCards();

        }

        private void AddCategoryCards()
        {
            CategoriesGrid.Children.Clear();
            foreach (KeyValuePair<string, Category> category in allCategories)
            {
                ThingsToDoCategoryCard card = new ThingsToDoCategoryCard(category.Value);
                CategoriesGrid.Children.Add(card);
            }
            
        }

        private void Sort()
        {
            if (SortType == "A-Z")
            {
                allCategories = allCategories.OrderBy(c => c.Key).ToList();
            }
            else if (SortType == "Z-A")
            {
                allCategories = allCategories.OrderByDescending(t => t.Key).ToList();
            }
            else if (SortType == "Recommended")
            {
                allCategories = allCategoriesCopy.ToList();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var split = ((ComboBox)sender).SelectedItem.ToString().Split(" ");
            SortType = split[1];
            if (split.Length > 2)
                SortType += " " + split[2];
            Sort();
            if (allCategories != null && CategoriesGrid?.Children?.Count > 0)
            {
                AddCategoryCards();
            }

        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Return)
            {
                var searchVal = SearchBar.Text;
                List<Attraction> searchResults = MainWindow.AttractionsList.FindAll(a => a.Name.Contains(searchVal, StringComparison.OrdinalIgnoreCase));
                if (Navigation.window.Width > 450)
                    Navigation.NavigateTo(new ThingsToDoSubsection(searchResults, searchVal));
                else
                    Navigation.NavigateToMobile(new ThingsToDoSubsection(searchResults, searchVal));

            }
        }

    }
}
