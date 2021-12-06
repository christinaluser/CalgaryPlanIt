using CalgaryPlanIt.Components;
using CalgaryPlanIt.Views;
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
        string Rbutton;
        string Sbutton;
        Tag SelectedTags = CalgaryPlanIt.Tag.None;
        List<Attraction> Attractions = new List<Attraction>();
        List<Attraction> TagAttractions = new List<Attraction>();
        List<Attraction> TmpAttractions = new List<Attraction>();

        public ThingsToDoSubsection(Category category)
        {
            InitializeComponent();
            Category = category;
            Attractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
            TmpAttractions = MainWindow.AttractionsList.FindAll(a => a.Category == Category);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            if (Rbutton == "LTH")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return x.Price.CompareTo(y.Price);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "HTL")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Price.CompareTo(x.Price);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "DT")
            {
                Attractions = Attractions.OrderBy(x => x.StartDate).ToList();
                //Attractions.Sort((x, y) => DateTime.Compare((DateTime)x.StartDate, (DateTime)y.StartDate));

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            else if (Rbutton == "MP")
            {
                Attractions.Sort(delegate (Attraction x, Attraction y)
                {

                    return y.Rating.CompareTo(x.Rating);
                });

                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }
            }
            if (Sbutton == "S5")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 5)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S4")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 4)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S3")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 3)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }
            else if (Sbutton == "S2")
            {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    if (att.Rating == 2)
                    {
                        AttractionsList.Children.Add(new AttractionCard(att));
                    }
                }
            }

            List<Attraction> TagAttractions = new List<Attraction>();
            if (SelectedTags != CalgaryPlanIt.Tag.None) {
                AttractionsList.Children.Clear();
                foreach (Attraction att in Attractions)
                {
                    Tag[] allTags = (Tag[])Enum.GetValues(typeof(Tag));

                    foreach (Tag tag in allTags)
                    {
                        if (SelectedTags.HasFlag(tag) && att.Tags.HasFlag(tag) && !TagAttractions.Contains(att))
                        {
                            TagAttractions.Add(att);

                        }

                    }

                }
                foreach (Attraction att in TagAttractions)
                {
                    AttractionsList.Children.Add(new AttractionCard(att));
                }

            }
            
             
        }

        private void LTH_Checked(object sender, RoutedEventArgs e)
        {   RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
           
        }
        
        private void HTL_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void MP_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void NY_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void DT_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Rbutton = li.Name.ToString();
        }

        private void S5_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();

        }

        private void S4_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        private void S3_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        private void S2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton li = sender as RadioButton;
            Sbutton = li.Name.ToString();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Attractions = TmpAttractions;
            AttractionsList.Children.Clear();
            foreach (Attraction attraction in Attractions)
            {
                AttractionsList.Children.Add(new AttractionCard(attraction));
            }
            
        }
    }
}
