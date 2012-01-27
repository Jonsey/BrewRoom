using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class RecipeHopViewModel : HopViewModel, IRecipeHopViewModel
    {
        private readonly IRecipeHop _hop;

        public RecipeHopViewModel(IRecipeHop hop) : base(hop)
        {
            _hop = hop;
        }

        public decimal Utilization
        {
            get { return _hop.Utilization; }
        }

        public decimal Ibu
        {
            get { return _hop.Ibu; }
        }

        public Weight Weight
        {
            get { return _hop.Weight; }
            set { _hop.Weight = value; }
        }

        public int BoilTime
        {
            get { return _hop.BoilTime; }
            set { _hop.BoilTime = value; }
        }
    }
}
