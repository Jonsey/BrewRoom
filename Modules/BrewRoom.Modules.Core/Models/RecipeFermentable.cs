using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeFermentable : Fermentable, IRecipeFermentable
    {
        #region Fields
        private readonly Recipe recipe;
        private readonly IFermentable fermentable;
        private Weight weight;
        //private readonly decimal pppg;
        #endregion

        #region Ctors
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
        //public new string Name
        //{
        //    get { return fermentable.Name; }
        //}

        public Weight Weight
        {
            get { return weight; }
        }

        //public new decimal Pppg
        //{
        //    get { return pppg; }
        //}

        //public decimal ExtractPoints
        //{
        //    get
        //    {
        //        return (pppg - 1) * 1000;
        //    }
        //}

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

        public void IncreaseWeight(Weight weightToAdd)
        {
            this.weight += weightToAdd;
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