using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeHop : Ingredient, IRecipeHop
    {
        #region Fields
        private readonly IHop hop;
        private Weight weight;
        private int boilTime;
        private readonly decimal alphaAcid;

        Recipe recipe; 
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
            set { weight = value; }
        }

        public virtual Decimal Ibu
        {
            get { return GetIbuContribution(); }
        }

        public virtual int BoilTime
        {
            get { return boilTime; }
            set { boilTime = value; }
        }

        public virtual decimal Utilization
        {
            get
            {
                return GetUtilization();
            }
        }
        #endregion

        #region Ctors
        protected RecipeHop()
        {
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, Recipe recipe)
            : base(hop.Name)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
            this.recipe = recipe;
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid, Recipe recipe)
            : base(hop.Name)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
            this.alphaAcid = alphaAcid;
            this.recipe = recipe;
        }

        #endregion

        #region Public Methods
        public virtual decimal GetAlphaAcid()
        {
            if (alphaAcid != default(decimal))
                return alphaAcid;

            return hop.GetAlphaAcid();
        }

        public virtual Weight GetWeight()
        {
            return weight;
        } 
        #endregion

        #region Private Methods
        decimal GetUtilization()
        {
            var gravity = recipe.GetStartingGravity();
            var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
            var timeFunction = ((1 - Math.Exp(-0.04 * boilTime)) / 4.15);

            return Math.Round((decimal)(gravityFunction * timeFunction), 1);
        }

        decimal GetIbuContribution()
        {
            var w = weight.ConvertTo(MassUnit.Grams).GetValue();
            var u = Utilization;
            var a = GetAlphaAcid() / 100;
            var v = recipe.GetBrewLength().ConvertTo(VolumeUnit.Litres).GetValue();

            return (w * u * a * 1000) / v;
        } 
        #endregion
    }
}