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
        private readonly IList<IRecipeHop> hops;
        private readonly IList<IRecipeFermentable> grains;
        private Volume brewLength; 
        #endregion

        #region Ctors
        public Recipe()
        {
            hops = new List<IRecipeHop>();
            grains = new List<IRecipeFermentable>();
        } 
        #endregion

        #region Properties
        public IList<IRecipeFermentable> Fermentables
        {
            get { return grains; }
        }

        public IEnumerable<IRecipeHop> Hops
        {
            get { return hops; }
        } 
        #endregion

        #region Public Methods

        #region Hops
        public void AddHop(IHop hop, Weight weight, int boilTime)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime, this));
        }

        public void AddHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime, alphaAcid, this));
        }

        public Weight GetTotalHopWeight()
        {
            var result = hops.Select(hop => hop.GetWeight())
                .AsParallel()
                .Aggregate(new Weight(0, MassUnit.Grams),
                           (current, weight) => current + weight.ConvertTo(MassUnit.Grams));

            return result;
        } 
        #endregion

        #region Grains
        public void AddFermentable(IFermentable fermentable, Weight weight)
        {
            if (grains.Count > 0)
            {
                var existing = grains.SingleOrDefault(x => x.Name == fermentable.Name);

                if (existing != null)
                    existing.IncreaseWeight(weight);
                else
                    grains.Add(new RecipeFermentable(this, fermentable, weight));
            }
            else
                grains.Add(new RecipeFermentable(this, fermentable, weight));
        }

        public void AddFermentable(IFermentable fermentable, Weight weight, decimal pppg)
        {
            if (grains.Count > 0)
            {
                var existing = grains.SingleOrDefault(x => x.Name == fermentable.Name && x.Pppg == pppg);

                if (existing != null)
                    existing.IncreaseWeight(weight);
                else
                    grains.Add(new RecipeFermentable(this, fermentable, weight, pppg));
            }
            else
                grains.Add(new RecipeFermentable(this, fermentable, weight, pppg));
        }

        public void RemoveFermentable(IRecipeFermentable fermentable)
        {
            grains.Remove(fermentable);
        }

        public Weight GetTotalGrainWeight()
        {
            var result = grains.Select(grain => grain.Weight)
                .AsParallel()
                .Aggregate(new Weight(0, MassUnit.KiloGrams),
                           (current, weight) => current + weight.ConvertTo(MassUnit.KiloGrams));

            return result;
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
            var result = hops.Sum(hop => hop.Ibu);

            return Math.Round(result, 1);
        }

        public decimal GetBuGuRatio()
        {
            var ibu = GetIbu();
            var startingGravityPoints = GetStartingGravityPoints();

            var buGuRatio = startingGravityPoints > 0 ? Math.Round(ibu / startingGravityPoints, 2) : 0;
            
            return buGuRatio;
        }
        #endregion

        #endregion
    }
}