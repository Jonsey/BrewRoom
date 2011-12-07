using System;
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
using BrewRoom.Modules.Core.Repositories;
using BrewRoom.Modules.Core.ViewModels;
using Microsoft.Practices.Unity;

namespace BrewRoom.Modules.Core.Views
{
    /// <summary>
    /// Interaction logic for StockItemsView.xaml
    /// </summary>
    public partial class StockItemsView : UserControl
    {
        public StockItemsView()
        {
            InitializeComponent();
        }

        [Dependency]
        public IStockItemsViewModel ViewModel
        {
            get { return DataContext as IStockItemsViewModel; }
            set { DataContext = value; }
        }

        private void Fermentables_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectFermentables.Execute();
        }

        private void Hops_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectHops.Execute();
        }
    }
}
