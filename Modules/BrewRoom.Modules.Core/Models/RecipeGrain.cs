using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeGrain
    {
        #region Fields
        private readonly Recipe recipe;
        private readonly IFermentable fermentable;
        private readonly Weight weight;
        private readonly decimal pppg;
        #endregion

        #region Ctors
        public RecipeGrain(Recipe recipe, IFermentable fermentable, Weight weight)
        {
            this.recipe = recipe;
            this.fermentable = fermentable;
            this.weight = weight;
            pppg = this.fermentable.Pppg;
        }

        public RecipeGrain(Recipe recipe, IFermentable fermentable, Weight weight, decimal pppg)
        {
            this.recipe = recipe;
            this.fermentable = fermentable;
            this.weight = weight;
            this.pppg = pppg;
        }
        #endregion

        #region Properties
        public string Name
        {
            get { return fermentable.Name; }
        }

        public Weight Weight
        {
            get { return weight; }
        }

        public decimal Pppg
        {
            get { return pppg; }
        }

        public decimal ExtractPoints
        {
            get
            {
                return (pppg - 1) * 1000;
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
            return (weight / recipe.GetTotalGrainWeight()).GetValue() * 100;
        }

        private decimal CalculateGravityContribution()
        {
            var points = (pppg - 1) * 1000;
            var pounds = weight.ConvertTo(MassUnit.Pounds).GetValue();
            var gallons = recipe.GetBrewLength().ConvertTo(VolumeUnit.Gallons).GetValue();

            var calculatedGravityContribution = (((pounds * points) / gallons) / 1000) + 1;
            return Math.Round(calculatedGravityContribution, 3);
        }


        private decimal CalculateGravityContributionInPoints()
        {
            var points = (pppg - 1) * 1000;
            var pounds = weight.ConvertTo(MassUnit.Pounds).GetValue();
            var gallons = recipe.GetBrewLength().ConvertTo(VolumeUnit.Gallons).GetValue();

            var calculatedGravityContribution = (pounds * points) / gallons;
            return Math.Round(calculatedGravityContribution, 3);
        }


        #endregion
    }
}