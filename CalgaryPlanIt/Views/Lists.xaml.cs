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
    /// Interaction logic for Lists.xaml
    /// </summary>
    public partial class Lists : Page
    {
        
        public Lists()
        {
            InitializeComponent();
            AddListCard();
        }

        private void AddListCard()
        {
            ListGrid.Children.Clear();
            foreach(Lis lis in MainWindow.ListofLists)
            {
                ListGrid.Children.Add(new ListCard(lis));
            }

        }
    }
}
