using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class Recipe
    {
        #region Fields
        private readonly IList<RecipeHop> hops;
        private readonly IList<RecipeGrain> grains;
        private Volume brewLength; 
        #endregion

        #region Ctors
        public Recipe()
        {
            hops = new List<RecipeHop>();
            grains = new List<RecipeGrain>();
        } 
        #endregion

        #region Properties
        public IList<RecipeGrain> Fermentables
        {
            get { return grains; }
        }

        public IList<RecipeHop> Hops
        {
            get { return hops; }
        } 
        #endregion

        #region Public Methods

        #region Hops
        public void AddHop(IHop hop, Weight weight, int boilTime)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime));
        }

        public void AddHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime, alphaAcid));
        }

        public Weight GetTotalHopWeight()
        {
            var result = new Weight(0, MassUnit.Grams);

            return hops.Select(hop => hop.GetWeight())
                .AsParallel()
                .Aggregate(result, (current, weight) => current + weight.ConvertTo(MassUnit.Grams));
        } 
        #endregion

        #region Grains
        public void AddFermentable(IFermentable fermentable, Weight weight)
        {
            grains.Add(new RecipeGrain(this, fermentable, weight));
        }

        public void AddFermentable(IFermentable fermentable, Weight weight, decimal pppg)
        {
            grains.Add(new RecipeGrain(this, fermentable, weight, pppg));
        }

        public void RemoveFermentable(RecipeGrain fermentable)
        {
            grains.Remove(fermentable);
        }

        public Weight GetTotalGrainWeight()
        {
            var result = new Weight(0, MassUnit.KiloGrams);

            return grains.Select(grain => grain.Weight)
                .AsParallel()
                .Aggregate(result, (current, weight) => current + weight.ConvertTo(MassUnit.KiloGrams));
        } 
        #endregion

        #region Brew Length
        public void SetBrewLength(Volume volume)
        {
            brewLength = volume;
        }

        public Volume GetBrewLength()
        {
            return brewLength;
        } 
        #endregion

        #region Gravity
        public decimal GetStartingGravity()
        {
            var result = 0M;
            Parallel.ForEach(grains, (grain) =>
                                          {
                                              result += grain.GravityContributionInPoints;
                                          });

            return Math.Round(1M + result / 1000M, 3);
        }

        public decimal GetStartingGravityPoints()
        {
            var result = 0M;
            Parallel.ForEach(grains, (grain) =>
            {
                result += grain.GravityContributionInPoints;
            });

            return Math.Round(result, 0);
        } 
        #endregion

        #region Bitterness
        public decimal GetIbu()
        {
            decimal result = 0;
            decimal gravity = GetStartingGravity();

            Parallel.ForEach(hops, hop => result += GetHopIbuContribution(hop, gravity));

            return Math.Round(result, 1);
        }

        private decimal GetHopIbuContribution(RecipeHop hop, decimal gravity)
        {
            var w = hop.GetWeight().ConvertTo(MassUnit.Grams).GetValue();
            var u = hop.GetUtilization(gravity);
            var a = hop.GetAlphaAcid() / 100;
            var v = GetBrewLength().ConvertTo(VolumeUnit.Litres).GetValue();

            return (w * u * a * 1000) / v;
        }

        public decimal GetBuGuRatio()
        {
            var ibu = GetIbu();

            return ibu > 0 ? Math.Round(ibu / GetStartingGravityPoints(), 2) : 0;
        }
        #endregion

        #endregion
    }
}