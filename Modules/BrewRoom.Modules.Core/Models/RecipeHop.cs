using System;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class RecipeHop
    {
        private readonly Hop _hop;
        private readonly Weight _weight;
        private readonly int _boilTime;
        private readonly decimal _alphaAcid;

        public RecipeHop(Hop hop, Weight weight, int boilTime)
        {
            _hop = hop;
            _weight = weight;
            _boilTime = boilTime;
        }

        public RecipeHop(Hop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            _hop = hop;
            _weight = weight;
            _boilTime = boilTime;
            _alphaAcid = alphaAcid;
        }

        public decimal GetAlphaAcid()
        {
            if (_alphaAcid != default(decimal))
                return _alphaAcid;

            return _hop.GetAlphaAcid();
        }

        public Weight GetWeight()
        {
            return _weight;
        }

        public decimal GetUtilization(decimal gravity)
        {
            var gravityFunction = 1.65 * Math.Pow(0.000125, ((double)gravity - 1));
            var timeFunction = ((1 - Math.Exp(-0.04 * _boilTime)) / 4.15);

            return Math.Round((decimal)(gravityFunction * timeFunction), 1);
        }
    }
}