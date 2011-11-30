using System;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeGrain
    {
        #region Fields
        private readonly Recipe _recipe;
        private readonly Grain _grain;
        private readonly Weight _weight;
        private readonly decimal _pppg;
        #endregion

        #region Ctors
        public RecipeGrain(Recipe recipe, Grain grain, Weight weight)
        {
            _recipe = recipe;
            _grain = grain;
            _weight = weight;
            _pppg = _grain.Pppg;
        }

        public RecipeGrain(Recipe recipe, Grain grain, Weight weight, decimal pppg)
        {
            _recipe = recipe;
            _grain = grain;
            _weight = weight;
            _pppg = pppg;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return _grain.Name; }
        }

        public Weight Weight
        {
            get { return _weight; }
        }

        public decimal Pppg
        {
            get { return _pppg; }
        }

        public decimal ExtractPoints
        {
            get
            {
                return (_pppg - 1) * 1000;
            }
        }

        public decimal GravityContribution
        {
            get { return CalculateGravityContribution(); }
        }

        public decimal GravityContributionInPoints
        {
            get { return CalculateGravityContributionInPoints(); }
        }

        public decimal PercentageOfMash
        {
            get { return CalculatePercenatgeofMash(); }
        }
        #endregion

        #region Private Methods
        private decimal CalculatePercenatgeofMash()
        {
            return (_weight / _recipe.GetTotalGrainWeight()).GetValue() * 100;
        }

        private decimal CalculateGravityContribution()
        {
            var points = (_pppg - 1) * 1000;
            var pounds = _weight.ConvertTo(MassUnit.Pounds).GetValue();
            var gallons = _recipe.GetBrewLength().ConvertTo(VolumeUnit.Gallons).GetValue();

            var calculatedGravityContribution = (((pounds * points) / gallons) / 1000) + 1;
            return Math.Round(calculatedGravityContribution, 3);
        }


        private decimal CalculateGravityContributionInPoints()
        {
            var points = (_pppg - 1) * 1000;
            var pounds = _weight.ConvertTo(MassUnit.Pounds).GetValue();
            var gallons = _recipe.GetBrewLength().ConvertTo(VolumeUnit.Gallons).GetValue();

            var calculatedGravityContribution = (pounds * points) / gallons;
            return Math.Round(calculatedGravityContribution, 3);
        }


        #endregion
    }
}