using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BrewRoom.Modules.Core.Models;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class EditRecipeViewModel : NotificationObject
    {
        private readonly Recipe _recipe;
        private readonly List<Grain> _fermentables;

        public IList<Grain> Fermentables { get { return _fermentables; } }

        public Decimal BrewLength
        {
            get { return _recipe.GetBrewLength().GetValue(); }
            set
            {
                _recipe.SetBrewLength(new Volume(value, BrewLengthUnit));
                RaisePropertyChanged("RecipePotential");
                RaisePropertyChanged("RecipeFermentables");
            }
        }

        public VolumeUnit BrewLengthUnit
        {
            get { return _recipe.GetBrewLength().GetUnit(); }
            set
            {
                _recipe.SetBrewLength(new Volume(BrewLength, value));
                RaisePropertyChanged("RecipePotential");
                RaisePropertyChanged("RecipeFermentables");
            }
        }

        public Decimal RecipePotential
        {
            get { return _recipe.GetStartingGravity(); }
        }

        public ObservableCollection<RecipeGrain> RecipeFermentables
        {
            get
            {
                var recipeGrains = _recipe.GetFermentables();

                return new ObservableCollection<RecipeGrain>(recipeGrains);
            }
        }

        public Grain SelectedFermentable { get; set; }

        public EditRecipeViewModel()
        {
            VolumeUnits = new List<VolumeUnit> {VolumeUnit.Litres, VolumeUnit.Gallons};
            _recipe = new Recipe();
            _recipe.SetBrewLength(20.Litres());

            _fermentables = new List<Grain>();
            var fermentable = new Grain("Marris Otter", 1.045M);
            _fermentables.Add(fermentable);

        }

        public List<VolumeUnit> VolumeUnits { get; private set; }

        private DelegateCommand<Grain> _addFermentableCommand;
        public DelegateCommand<Grain> AddFermentableCommand
        {
            get { return _addFermentableCommand ?? (_addFermentableCommand = new DelegateCommand<Grain>(AddFermentable)); }
        }

        private void AddFermentable(Grain fermentable)
        {
            if (SelectedFermentable != null)
            {
                _recipe.AddGrain(SelectedFermentable, 1.KiloGram());
                RaisePropertyChanged("RecipeFermentables");
                RaisePropertyChanged("RecipePotential");
                
            }
        }
    }
}