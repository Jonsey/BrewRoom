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
        #region Fields
        readonly IRecipeRepository _recipeRepository;
        private readonly Recipe _recipe;
        #endregion

        #region Properties

        public string Name
        {
            get { return _recipe.Name; }
            set { _recipe.Name = value; }
        }

        public IStockItemsViewModel StockItemsViewModel { get; set; }

        public List<VolumeUnit> VolumeUnits { get; private set; }

        public Decimal BrewLength
        {
            get { return _recipe.GetBrewLength().GetValue(); }
            set
            {
                _recipe.SetBrewLength(new Volume(value, BrewLengthUnit));
                UpdateRecipeProperties();
            }
        }

        public VolumeUnit BrewLengthUnit
        {
            get { return _recipe.GetBrewLength().GetUnit(); }
            set
            {
                _recipe.SetBrewLength(new Volume(BrewLength, value));
                UpdateRecipeProperties();
            }
        }

        public Weight RecipeTotalGrainWeight
        {
            get { return _recipe.GetTotalGrainWeight(); }
        }

        public Weight RecipeTotalHopWeight
        {
            get { return _recipe.GetTotalHopWeight(); }
        }

        public decimal RecipeBuGu
        {
            get { return _recipe.GetBuGuRatio(); }
        }

        public decimal RecipeBitterness
        {
            get { return _recipe.GetIbu(); }
        }

        public Decimal RecipePotential
        {
            get { return _recipe.GetStartingGravity(); }
        }

        public ObservableCollection<IRecipeFermentable> RecipeFermentables
        {
            get
            {
                var recipeGrains = _recipe.Fermentables;

                return new ObservableCollection<IRecipeFermentable>(recipeGrains);
            }
        }

        public ObservableCollection<IRecipeHopViewModel> RecipeHops
        {
            get
            {
                var recipeHops = _recipe.Hops;
                var query = from hop in recipeHops
                            select new RecipeHopViewModel(hop);

                return new ObservableCollection<IRecipeHopViewModel>(query);
            }
        }

        private IRecipeFermentable _selectedFermentable;
        public IRecipeFermentable SelectedFermentable
        {
            get { return _selectedFermentable; }
            set
            {
                _selectedFermentable = value;
                _selectedHop = null;
                RaisePropertyChanged("SelectedHop");
                RaisePropertyChanged("SelectedFermentable");
                ShowFermentableDetails();
            }
        }

        private IRecipeHopViewModel _selectedHop;
        public IRecipeHopViewModel SelectedHop
        {
            get { return _selectedHop; }
            set
            {
                _selectedHop = value;
                _selectedFermentable = null;
                RaisePropertyChanged("SelectedHop");
                RaisePropertyChanged("SelectedFermentable");
                ShowHopDetails();
            }
        }

        private void ShowFermentableDetails()
        {
            IsFermentableDetailsVisible = true;
            IsHopDetailsVisible = false;

            RaisePropertyChanged("IsFermentableDetailsVisible"); // TODO not tested
            RaisePropertyChanged("IsHopDetailsVisible"); // TODO not tested
        }

        private void ShowHopDetails()
        {
            IsFermentableDetailsVisible = false;
            IsHopDetailsVisible = true;

            RaisePropertyChanged("IsFermentableDetailsVisible"); // TODO not tested
            RaisePropertyChanged("IsHopDetailsVisible"); // TODO not tested
        }

        public bool IsFermentableDetailsVisible { get; private set; }

        public bool IsHopDetailsVisible { get; private set; }

        #endregion

        #region Ctor
        public EditRecipeViewModel(IEventAggregator eventAggregator, IStockItemsViewModel stockItemsViewModel, IRecipeRepository recipeRepository)
        {
            this._recipeRepository = recipeRepository;
            StockItemsViewModel = stockItemsViewModel;

            VolumeUnits = new List<VolumeUnit> { VolumeUnit.Litres, VolumeUnit.Gallons };
            _recipe = new Recipe();
            _recipe.SetBrewLength(20.Litres());

            eventAggregator.GetEvent<AddHopToRecipeEvent>().Subscribe(AddHop);
            eventAggregator.GetEvent<AddFermentableToRecipeEvent>().Subscribe(AddFermentable);
        }

        #endregion

        #region Commands
        public DelegateCommand RemoveFermentableCommand
        {
            get { return new DelegateCommand(RemoveRecipeFermentable); }
        }

        public DelegateCommand SaveRecipeCommand
        {
            get
            {
                return new DelegateCommand(SaveRecipe);
            }
        }

        public DelegateCommand UpdateFermentableCommand
        {
            get { return new DelegateCommand(UpdateFermentable); }
        }

        public DelegateCommand UpdateHopCommand
        {
            get { return new DelegateCommand(UpdateHop); }
        }

        #endregion

        #region Private Methods

        void AddHop(IHopViewModel hopViewModel)
        {
            var hop = hopViewModel.Model;
            _recipe.AddHop(hop, 5.Grams(), 60);

            UpdateRecipeProperties(); // todo: not covered
        }

        void UpdateHop()
        {
            UpdateRecipeProperties();
        }

        void AddFermentable(IFermentableViewModel fermentableViewModel)
        {
            var fermentable = fermentableViewModel.Model;
            _recipe.AddFermentable(fermentable, 1.KiloGram(), fermentableViewModel.Pppg);

            UpdateRecipeProperties(); // todo: not covered
        }

        void UpdateFermentable()
        {
            UpdateRecipeProperties();
        }

        void RemoveRecipeFermentable()
        {
            _recipe.RemoveFermentable(SelectedFermentable);
            UpdateRecipeProperties();
        }

        void SaveRecipe()
        {
            _recipeRepository.Save(_recipe);
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