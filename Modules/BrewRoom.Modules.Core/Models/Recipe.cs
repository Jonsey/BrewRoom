using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrewRoom.Modules.Core.Interfaces.Models;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class Recipe : EntityBase, IRecipe
    {
        #region Fields
        private IList<IRecipeHop> hops;
        private IList<IRecipeFermentable> grains;
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
        public virtual IList<IRecipeFermentable> Fermentables
        {
            get { return grains; }
            private set { grains = value; }
        }

        public virtual IList<IRecipeHop> Hops
        {
            get { return hops; }
            private set { hops = value; }
        }

        public virtual String Name { get; set; }

        #endregion

        #region Public Methods

        #region Hops
        public virtual void AddHop(IHop hop, Weight weight, int boilTime)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime, this));
        }

        public virtual void AddHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid)
        {
            hops.Add(new RecipeHop(hop, weight, boilTime, alphaAcid, this));
        }

        public virtual Weight GetTotalHopWeight()
        {
            var result = hops.Select(hop => hop.GetWeight())
                .AsParallel()
                .Aggregate(new Weight(0, MassUnit.Grams),
                           (current, weight) => current + weight.ConvertTo(MassUnit.Grams));

            return result;
        } 
        #endregion

        #region Grains
        public virtual void AddFermentable(IFermentable fermentable, Weight weight)
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

        public virtual void AddFermentable(IFermentable fermentable, Weight weight, decimal pppg)
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

        public virtual void RemoveFermentable(IRecipeFermentable fermentable)
        {
            grains.Remove(fermentable);
        }

        public virtual Weight GetTotalGrainWeight()
        {
            var result = grains.Select(grain => grain.Weight)
                .AsParallel()
                .Aggregate(new Weight(0, MassUnit.KiloGrams),
                           (current, weight) => current + weight.ConvertTo(MassUnit.KiloGrams));

            return result;
        } 
        #endregion

        #region Brew Length
        public virtual void SetBrewLength(Volume volume)
        {
            brewLength = volume;
        }

        public virtual Volume GetBrewLength()
        {
            return brewLength;
        } 
        #endregion

        #region Gravity
        public virtual decimal GetStartingGravity()
        {
            var result = 0M;
            Parallel.ForEach(grains, (grain) =>
                                         {
                                             result += grain.GravityContributionInPoints;
                                         });

            return Math.Round(1M + result / 1000M, 3);
        }

        public virtual decimal GetStartingGravityPoints()
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
        public virtual decimal GetIbu()
        {
            var result = hops.Sum(hop => hop.Ibu);

            return Math.Round(result, 1);
        }

        public virtual decimal GetBuGuRatio()
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