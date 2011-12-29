using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IRecipe
    {
        IList<IRecipeFermentable> Fermentables { get; }
        IList<IRecipeHop> Hops { get; }
        String Name { get; }
        void AddHop(IHop hop, Weight weight, int boilTime);
        void AddHop(IHop hop, Weight weight, int boilTime, decimal alphaAcid);
        Weight GetTotalHopWeight();
        void AddFermentable(IFermentable fermentable, Weight weight);
        void AddFermentable(IFermentable fermentable, Weight weight, decimal pppg);
        void RemoveFermentable(IRecipeFermentable fermentable);
        Weight GetTotalGrainWeight();
        void SetBrewLength(Volume volume);
        Volume GetBrewLength();
        decimal GetStartingGravity();
        decimal GetStartingGravityPoints();
        decimal GetIbu();
        decimal GetBuGuRatio();
    }
}
