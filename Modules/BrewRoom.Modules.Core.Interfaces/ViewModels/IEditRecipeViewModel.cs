using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrewRoom.Modules.Core.Interfaces.Models;
using Microsoft.Practices.Prism.Commands;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IEditRecipeViewModel
    {
        //IIngredientViewModel SelectedStockItem { get; set; }
        IStockItemsViewModel StockItemsViewModel { get; set; }
        List<VolumeUnit> VolumeUnits { get; }
        Decimal BrewLength { get; set; }
        VolumeUnit BrewLengthUnit { get; set; }
        Weight RecipeTotalGrainWeight { get; }
        decimal RecipeBuGu { get; }
        Decimal RecipePotential { get; }
        ObservableCollection<IRecipeFermentable> RecipeFermentables { get; }
        ObservableCollection<IRecipeHopViewModel> RecipeHops { get; }
        IRecipeFermentable SelectedFermentable { get; set; }
        DelegateCommand RemoveFermentableCommand { get; }
        Weight RecipeTotalHopWeight { get; }
        decimal RecipeBitterness { get; }
    }
}