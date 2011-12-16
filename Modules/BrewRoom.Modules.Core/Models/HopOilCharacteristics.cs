using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public class HopOilCharacteristics : IHopOilCharacteristics
    {
        public virtual decimal PercentageOfTotalWeight { get; set; }

        public virtual decimal Farnesene { get; set; }

        public virtual decimal Carophyllene { get; set; }

        public virtual decimal Myrcene { get; set; }

        public virtual decimal Humulene { get; set; }

        public virtual decimal OtherAcids { get; set; }

        public virtual decimal TotalAlphaAcid { get; set; }

        public virtual bool AreValid()
        {
            return (Farnesene + Carophyllene + Myrcene + Humulene + OtherAcids) == 100;
        }
    }
}