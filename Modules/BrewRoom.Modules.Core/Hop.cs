using System;

namespace BrewRoom.Modules.Core
{
    public class Hop : Ingredient
    {
        private HopOilCharacteristics _characteristics;

        #region Properties

        #endregion

        #region Ctor

        public Hop(string name)
            : base(name)
        {
        }

        #endregion

        public void AddOilCharacteristics(HopOilCharacteristics characteristics)
        {
            if (!characteristics.AreValid())
                throw new ArgumentException("Characteristics are invalid. See innerexception for details.");
            
            _characteristics = characteristics;
        }

        public HopOilCharacteristics GetCharacteristics()
        {
            return _characteristics;
        }

        public decimal GetAlphaAcid()
        {
            return _characteristics.TotalAlphaAcid;
        }
    }
}
