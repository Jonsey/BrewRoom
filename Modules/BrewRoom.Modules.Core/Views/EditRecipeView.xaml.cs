﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Interfaces.Views;
using BrewRoom.Modules.Core.ViewModels;
using Microsoft.Practices.Unity;

namespace BrewRoom.Modules.Core.Views
{
    /// <summary>
    /// Interaction logic for EditRecipeView.xaml
    /// </summary>
    public partial class EditRecipeView : UserControl, IEditRecipeView
    {
        public EditRecipeView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IEditRecipeViewModel ViewModel
        {
            get { return DataContext as IEditRecipeViewModel; }
            set { DataContext = value; }
        }
    }
}
