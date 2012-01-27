using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeHop : Hop, IRecipeHop
    {
        #region Fields
        private readonly IHop hop;
        private readonly decimal alphaAcid;

        #endregion

        #region Properties

        public virtual Recipe Recipe { get; private set; }

        public virtual Weight Weight { get; set; }

        public virtual int BoilTime { get; set; }

        public virtual Decimal Ibu
        {
            get { return GetIbuContribution(); }
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
        protected RecipeHop() : base()
        {
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, Recipe recipe)
            : base(hop.Name)
        {
            //this.hop = hop;
            this.Name = hop.Name;
            this.Description = hop.Description;
            this.AddOilCharacteristics(hop.GetCharacteristics());
            this.Weight = weight;
            this.BoilTime = boilTime;
            this.Recipe  = recipe;
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid, Recipe recipe)
            : base(hop.Name)
        {
            this.hop = hop;
            this.Weight = weight;
            this.BoilTime = boilTime;
            this.alphaAcid = alphaAcid;
            this.Recipe = recipe;
        }

        #endregion

        #region Public Methods
        public override decimal GetAlphaAcid()
        {
            return alphaAcid != default(decimal) ? alphaAcid : base.GetAlphaAcid();
        }

        public virtual Weight GetWeight()
        {
            return Weight;
        } 
        #endregion

        #region Private Methods
        decimal GetUtilization()
        {
            var gravity = Recipe.GetStartingGravity();
            var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
            var timeFunction = ((1 - Math.Exp(-0.04 * BoilTime)) / 4.15);

            return Math.Round((decimal)(gravityFunction * timeFunction), 1);
        }

        decimal GetIbuContribution()
        {
            var w = Weight.ConvertTo(MassUnit.Grams).GetValue();
            var u = Utilization;
            var a = GetAlphaAcid() / 100;
            var v = Recipe.GetBrewLength().ConvertTo(VolumeUnit.Litres).GetValue();

            return (w * u * a * 1000) / v;
        } 
        #endregion
    }
}