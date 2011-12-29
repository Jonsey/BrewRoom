using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BrewRoom.Modules.Core.Events;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.Repositories;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class EditRecipeViewModel : NotificationObject, IEditRecipeViewModel
    {
        readonly IRecipeRepository recipeRepository;
        public IStockItemsViewModel StockItemsViewModel { get; set; }

        #region Fields
        private readonly Recipe recipe;
        #endregion

        #region Properties
        public List<VolumeUnit> VolumeUnits { get; private set; }

        public Decimal BrewLength
        {
            get { return recipe.GetBrewLength().GetValue(); }
            set
            {
                recipe.SetBrewLength(new Volume(value, BrewLengthUnit));
                UpdateRecipeProperties();
            }
        }

        public VolumeUnit BrewLengthUnit
        {
            get { return recipe.GetBrewLength().GetUnit(); }
            set
            {
                recipe.SetBrewLength(new Volume(BrewLength, value));
                UpdateRecipeProperties();
            }
        }

        public Weight RecipeTotalGrainWeight
        {
            get { return recipe.GetTotalGrainWeight(); }
        }

        public Weight RecipeTotalHopWeight
        {
            get { return recipe.GetTotalHopWeight(); }
        }

        public decimal RecipeBuGu
        {
            get { return recipe.GetBuGuRatio(); }
        }

        public decimal RecipeBitterness
        {
            get { return recipe.GetIbu(); }
        }

        public Decimal RecipePotential
        {
            get { return recipe.GetStartingGravity(); }
        }

        public ObservableCollection<IRecipeFermentable> RecipeFermentables
        {
            get
            {
                var recipeGrains = recipe.Fermentables;

                return new ObservableCollection<IRecipeFermentable>(recipeGrains);
            }
        }

        public ObservableCollection<IRecipeHop> RecipeHops
        {
            get
            {
                var recipeHops = recipe.Hops;

                return new ObservableCollection<IRecipeHop>(recipeHops);
            }
        }

        public IRecipeFermentable SelectedRecipeFermentable { get; set; }

        public IIngredientViewModel SelectedStockItem { get; set; }

        #endregion

        #region Ctor
        public EditRecipeViewModel(IEventAggregator eventAggregator, IStockItemsViewModel stockItemsViewModel, IRecipeRepository recipeRepository)
        {
            this.recipeRepository = recipeRepository;
            StockItemsViewModel = stockItemsViewModel;

            VolumeUnits = new List<VolumeUnit> { VolumeUnit.Litres, VolumeUnit.Gallons };
            recipe = new Recipe();
            recipe.SetBrewLength(20.Litres());

            eventAggregator.GetEvent<StockItemSelectedEvent>().Subscribe(StockItemSelectedEventHandler);
        }

        #endregion

        #region Commands
        public DelegateCommand RemoveFermentableCommand
        {
            get { return new DelegateCommand(RemoveRecipeFermentable); }
        }

        public DelegateCommand AddSelectedStockItemCommand
        {
            get
            {
                return new DelegateCommand(AddSelectedStockItem);
            }
        }

        public DelegateCommand SaveRecipeCommand
        {
            get
            {
                return new DelegateCommand(SaveRecipe);
            }
        }
        #endregion

        #region Private Methods
        void AddSelectedStockItem()
        {
            if (SelectedStockItem == null) return;

            if (SelectedStockItem is IHopViewModel)
            {
                AddHop();
            }
            else if (SelectedStockItem is IFermentableViewModel)
            {
                AddFermentable();
            }

            UpdateRecipeProperties();
        }

        void AddHop()
        {
            var hopViewModel = SelectedStockItem as IHopViewModel;
            var hop = hopViewModel.Model;

            recipe.AddHop(hop, 5.Grams(), 60);
        }

        void AddFermentable()
        {
            var fermentableViewModel = SelectedStockItem as IFermentableViewModel;
            var fermentable = fermentableViewModel.Model;

            recipe.AddFermentable(fermentable, 1.KiloGram(), fermentableViewModel.Pppg);
        }


        void RemoveRecipeFermentable()
        {
            recipe.RemoveFermentable(SelectedRecipeFermentable);
            UpdateRecipeProperties();
        }

        void SaveRecipe()
        {
            recipeRepository.Save(recipe);
        }

        void StockItemSelectedEventHandler(IIngredientViewModel item)
        {
            SelectedStockItem = item;
        }

        void UpdateRecipeProperties()
        {
            RaisePropertyChanged("RecipeFermentables");
            RaisePropertyChanged("RecipeHops");
            RaisePropertyChanged("RecipeTotalGrainWeight");
            RaisePropertyChanged("RecipeTotalHopWeight");
            RaisePropertyChanged("RecipePotential");
            RaisePropertyChanged("RecipeBuGu");
            RaisePropertyChanged("RecipeBitterness");
        }
        #endregion
    }
}