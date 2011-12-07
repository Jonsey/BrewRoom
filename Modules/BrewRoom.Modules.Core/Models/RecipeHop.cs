using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeHop
    {
        private readonly IHop hop;
        private readonly Weight weight;
        private readonly int boilTime;
        private readonly decimal alphaAcid;

        public String Name
        {
            get { return hop.Name; }
        }

        public Weight Weight
        {
            get { return GetWeight(); }
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
        }

        public RecipeHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            this.hop = hop;
            this.weight = weight;
            this.boilTime = boilTime;
            this.alphaAcid = alphaAcid;
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

        public decimal GetUtilization(decimal gravity)
        {
            var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
            var timeFunction = ((1 - Math.Exp(-0.04 * boilTime)) / 4.15);

            return Math.Round((decimal)(gravityFunction * timeFunction), 1);
        }
    }
}