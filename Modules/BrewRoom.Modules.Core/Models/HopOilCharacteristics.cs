using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public class HopOilCharacteristics : IHopOilCharacteristics
    {
        public decimal PercentageOfTotalWeight { get; set; }

        public decimal Farnesene { get; set; }

        public decimal Carophyllene { get; set; }

        public decimal Myrcene { get; set; }

        public decimal Humulene { get; set; }

        public decimal OtherAcids { get; set; }

        public decimal TotalAlphaAcid { get; set; }

        public bool AreValid()
        {
            return (Farnesene + Carophyllene + Myrcene + Humulene + OtherAcids) == 100;
        }
    }
}