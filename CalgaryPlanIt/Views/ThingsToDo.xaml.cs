﻿using CalgaryPlanIt.Components;
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
        public ThingsToDo()
        {
            InitializeComponent();
            AddCategoryCards();
        }

        private void AddCategoryCards()
        {
            CategoriesGrid.Children.Clear();
            Category[] allCategories = (Category[])Enum.GetValues(typeof(Category));
            foreach (Category category in allCategories)
            {
                ThingsToDoCategoryCard card = new ThingsToDoCategoryCard(category);
                CategoriesGrid.Children.Add(card);
            }
            
        }
    }
}
