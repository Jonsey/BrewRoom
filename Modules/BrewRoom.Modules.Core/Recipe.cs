using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewRoom.Modules.Core.ValueObjects;

namespace BrewRoom.Modules.Core
{
    public class Recipe
    {
        private readonly IList<RecipeHop> _hops;
        private IList<RecipeGrain> _grains;
        private Volume _brewLength;

        public Recipe()
        {
            _hops = new List<RecipeHop>();
            _grains = new List<RecipeGrain>();
        }

        public void AddHop(Hop hop, Weight weight, int boilTime)
        {
            _hops.Add(new RecipeHop(hop, weight, boilTime));
        }


        public void AddHop(Hop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            _hops.Add(new RecipeHop(hop, weight, boilTime, alphaAcid));

        }


        public Weight GetTotalHopWeight()
        {
            var result = new Weight(0, MassUnit.Grams);

            return _hops.Select(hop => hop.GetWeight())
                .AsParallel()
                .Aggregate(result, (current, weight) => current + weight.ConvertTo(MassUnit.Grams));
        }

        public void AddGrain(Grain grain1, Weight weight)
        {
            _grains.Add(new RecipeGrain(grain1, weight));
        }

        public void AddGrain(Grain grain, Weight weight, decimal pppg)
        {
            _grains.Add(new RecipeGrain(grain, weight, pppg));
        }

        public Weight GetTotalGrainWeight()
        {
            var result = new Weight(0, MassUnit.KiloGrams);

            return _grains.Select(grain => grain.GetWeight())
                .AsParallel()
                .Aggregate(result, (current, weight) => current + weight.ConvertTo(MassUnit.KiloGrams));
        }

        public void SetBrewLength(Volume volume)
        {
            _brewLength = volume;
        }

        public Volume GetBrewLength()
        {
            return _brewLength;
        }

        public decimal GetStartingGravity()
        {
            var result = 0M;
            var gallons = _brewLength.ConvertTo(VolumeUnit.Gallons);

            Parallel.ForEach(_grains, (grain) =>
                                          {
                                              var points = grain.GetExtractPoints();
                                              var pounds = grain.GetWeight().ConvertTo(MassUnit.Pounds);

                                              result += points / pounds.GetValue();
                                          });

            var gravityUnits = result * gallons.GetValue();

            return Math.Round(1M + result / 1000M, 3);
        }

        public decimal GetIBU()
        {
            decimal result = 0;
            decimal gravity = GetStartingGravity();

            Parallel.ForEach(_hops, hop => result += GetHopIBUContribution(hop, gravity));

            return Math.Round(result, 1);
        }

        private decimal GetHopIBUContribution(RecipeHop hop, decimal gravity)
        {
            var w = hop.GetWeight().ConvertTo(MassUnit.Grams).GetValue();
            var u = hop.GetUtilization(gravity);
            var a = hop.GetAlphaAcid() / 100;
            var v = GetBrewLength().ConvertTo(VolumeUnit.Litres).GetValue();

            return (w * u * a * 1000) / v;
        }

        public object GetGuBuRatio()
        {
            return GetStartingGravity() / GetIBU();
        }
    }
}