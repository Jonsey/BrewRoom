namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IHopOilCharacteristics
    {
        decimal PercentageOfTotalWeight { get; set; }
        decimal Farnesene { get; set; }
        decimal Carophyllene { get; set; }
        decimal Myrcene { get; set; }
        decimal Humulene { get; set; }
        decimal OtherAcids { get; set; }
        decimal TotalAlphaAcid { get; set; }
        bool AreValid();
    }
}