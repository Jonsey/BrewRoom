using System;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IRecipeHop
    {
        String Name { get; }
        Weight Weight { get; }
        decimal Utilization { get; }
        Decimal Ibu { get; }
        int BoilTime { get; set; }

        decimal GetAlphaAcid();
        Weight GetWeight();      
    }
}