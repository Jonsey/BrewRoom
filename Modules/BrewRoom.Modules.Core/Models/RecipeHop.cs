using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeHop : IRecipeHop
    {
        private readonly IHop hop;
        private Weight weight;
        private int boilTime;
        private readonly decimal alphaAcid;

        Recipe recipe;

        public String Name
        {
            get { return hop.Name; }
        }

        public Weight Weight
        {
            get { return weight; }
            set { weight = value; }
        }

        public Decimal Ibu
        {
            get { return GetIbuContribution(); }
        }

        public int BoilTime
        {
            get { return boilTime; }
            set { boilTime = value; }
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, Recipe recipe)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
            this.recipe = recipe;
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid, Recipe recipe)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
            this.alphaAcid = alphaAcid;
            this.recipe = recipe;
        }

        public decimal GetAlphaAcid()
        {
            if (alphaAcid != default(decimal))
                return alphaAcid;

            return hop.GetAlphaAcid();
        }

        public Weight GetWeight()
        {
            return weight;
        }

        private decimal GetIbuContribution()
        {
            var w = weight.ConvertTo(MassUnit.Grams).GetValue();
            var u = Utilization;
            var a = GetAlphaAcid() / 100;
            var v = recipe.GetBrewLength().ConvertTo(VolumeUnit.Litres).GetValue();

            return (w * u * a * 1000) / v;
        }

        public decimal GetUtilization(decimal gravity)
        {
            var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
            var timeFunction = ((1 - Math.Exp(-0.04 * boilTime)) / 4.15);

            return Math.Round((decimal)(gravityFunction * timeFunction), 1);
        }

        public decimal Utilization
        {
            get
            {
                var gravity = recipe.GetStartingGravity();
                var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
                var timeFunction = ((1 - Math.Exp(-0.04 * boilTime)) / 4.15);

                return Math.Round((decimal)(gravityFunction * timeFunction), 1);
            }
        }
    }
}