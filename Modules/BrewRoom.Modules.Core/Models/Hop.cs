using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.ViewModels;

namespace BrewRoom.Modules.Core.Models
{
    public class Hop : Ingredient, IHop
    {
        private IHopOilCharacteristics characteristics;

        #region Properties
        public decimal AlphaAcid
        {
            get { return GetAlphaAcid(); }
        }
        #endregion

        #region Ctor

        public Hop(string name)
            : base(name)
        {
        }

        #endregion

        public void AddOilCharacteristics(IHopOilCharacteristics hopCharacteristics)
        {
            if (!hopCharacteristics.AreValid())
                throw new ArgumentException("Characteristics are invalid. See innerexception for details.");
            
            this.characteristics = hopCharacteristics;
        }

        public IHopOilCharacteristics GetCharacteristics()
        {
            return characteristics;
        }

        public decimal GetAlphaAcid()
        {
            return characteristics.TotalAlphaAcid;
        }
    }
}
