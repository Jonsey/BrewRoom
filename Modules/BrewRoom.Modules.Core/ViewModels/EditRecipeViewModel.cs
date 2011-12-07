using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrewRoom.Modules.Core.Events;
using BrewRoom.Modules.Core.Interfaces.Models;
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

        public decimal RecipeBuGu
        {
            get { return recipe.GetBuGuRatio(); }
        }

        public Decimal RecipePotential
        {
            get { return recipe.GetStartingGravity(); }
        }

        public ObservableCollection<RecipeGrain> RecipeFermentables
        {
            get
            {
                var recipeGrains = recipe.Fermentables;

                return new ObservableCollection<RecipeGrain>(recipeGrains);
            }
        }

        public ObservableCollection<RecipeHop> RecipeHops
        {
            get
            {
                var recipeHops = recipe.Hops;

                return new ObservableCollection<RecipeHop>(recipeHops);
            }
        }

        public IFermentableViewModel SelectedStockFermentable { get; set; }

        public RecipeGrain SelectedRecipeFermentable { get; set; }

        public IHop SelectedHop { get; set; } 
        #endregion

        #region Ctor
        public EditRecipeViewModel(IEventAggregator eventAggregator, IStockItemsViewModel stockItemsViewModel)
        {
            StockItemsViewModel = stockItemsViewModel;

            VolumeUnits = new List<VolumeUnit> { VolumeUnit.Litres, VolumeUnit.Gallons };
            recipe = new Recipe();
            recipe.SetBrewLength(20.Litres());

            eventAggregator.GetEvent<StockFermentableSelectedEvent>().Subscribe(StockFermentableSelectedEventHandler);
        }

        void StockFermentableSelectedEventHandler(IFermentableViewModel obj)
        {
            SelectedStockFermentable = obj;
        }

        #endregion

        #region Commands
        private DelegateCommand _addFermentableCommand;
        public DelegateCommand AddFermentableCommand
        {
            get { return _addFermentableCommand ?? (_addFermentableCommand = new DelegateCommand(AddFermentable)); }
        }

        private DelegateCommand<Hop> _addHopCommand;
        public DelegateCommand<Hop> AddHopCommand
        {
            get { return _addHopCommand ?? (_addHopCommand = new DelegateCommand<Hop>(AddHop)); }
        }

        public DelegateCommand RemoveFermentableCommand
        {
            get { return new DelegateCommand(RemoveRecipeFermentable); }
        }

        void RemoveRecipeFermentable()
        {
            recipe.RemoveFermentable(SelectedRecipeFermentable);
            UpdateRecipeProperties();
        }

        #endregion

        #region Private Methods
        void AddHop(IHop hop)
        {
            if (SelectedHop == null) return;

            recipe.AddHop(SelectedHop, 5.Grams(), 60);
            UpdateRecipeProperties();
        }

        void AddFermentable()
        {
            if (SelectedStockFermentable == null) return;

            recipe.AddFermentable(SelectedStockFermentable.Model, 1.KiloGram());
            UpdateRecipeProperties();
        }

        void UpdateRecipeProperties()
        {
            RaisePropertyChanged("RecipeFermentables");
            RaisePropertyChanged("RecipePotential");
            RaisePropertyChanged("RecipeTotalGrainWeight");
            RaisePropertyChanged("RecipeHops");
            RaisePropertyChanged("RecipeBuGu");
        } 
        #endregion
    }
}