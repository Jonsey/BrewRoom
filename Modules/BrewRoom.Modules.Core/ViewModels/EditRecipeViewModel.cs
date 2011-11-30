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
        private readonly List<Hop> _hops;

        public IList<Grain> Fermentables { get { return _fermentables; } }
        public IList<Hop> Hops { get { return _hops; } }

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

        public decimal RecipeBuGu
        {
            get { return _recipe.GetGuBuRatio(); }
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
            VolumeUnits = new List<VolumeUnit> { VolumeUnit.Litres, VolumeUnit.Gallons };
            _recipe = new Recipe();
            _recipe.SetBrewLength(20.Litres());

            _fermentables = new List<Grain>();
            var fermentable1 = new Grain("Marris Otter", 1.045M);
            _fermentables.Add(fermentable1);
            var fermentable2 = new Grain("Pils Malt", 1.038M);
            _fermentables.Add(fermentable2);

            var hop = new Hop("Saaz");
            hop.AddOilCharacteristics(new HopOilCharacteristics
                                          {
                                              Carophyllene = 20M,
                                              Farnesene = 20M,
                                              Humulene = 20M,
                                              Myrcene = 20M,
                                              OtherAcids = 20M,
                                              PercentageOfTotalWeight = 20,
                                              TotalAlphaAcid = 5M
                                          });
            _hops = new List<Hop>
                        {
                            hop
                        };
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
                UpdateRecipeProperties();
            }
        }

        void UpdateRecipeProperties()
        {
            RaisePropertyChanged("RecipeFermentables");
            RaisePropertyChanged("RecipePotential");
            RaisePropertyChanged("RecipeTotalGrainWeight");
            RaisePropertyChanged("RecipeBuGu");
        }
    }
}