using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeFermentable : Fermentable, IRecipeFermentable
    {
        #region Fields
        private Recipe recipe;
        private readonly IFermentable fermentable;
        private Weight weight;
        #endregion

        #region Ctors
        protected RecipeFermentable()
        {
        }

        public RecipeFermentable(Recipe recipe, IFermentable fermentable, Weight weight)
            : base(fermentable.Name)
        {
            this.recipe = recipe;
            this.fermentable = fermentable;
            this.weight = weight;
            this.pppg = this.fermentable.Pppg;
        }

        public RecipeFermentable(Recipe recipe, IFermentable fermentable, Weight weight, decimal pppg) 
            : base(fermentable.Name)
        {
            this.recipe = recipe;
            this.fermentable = fermentable;
            this.weight = weight;
            this.pppg = pppg;
        }
        #endregion

        #region Properties

        public virtual Recipe Recipe
        {
            get { return recipe; }
            private set { recipe = value; }
        }

        public virtual Weight Weight
        {
            get { return weight; }
            private set { weight = value; }
        }

        public virtual decimal GravityContribution
        {
            get { return CalculateGravityContribution(); }
        }

        public virtual decimal GravityContributionInPoints
        {
            get { return CalculateGravityContributionInPoints(); }
        }

        public virtual decimal PercentageOfMash
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

        #region Public Methods
        public virtual void IncreaseWeight(Weight weightToAdd)
        {
            this.weight += weightToAdd;
        }
        #endregion
    }
}