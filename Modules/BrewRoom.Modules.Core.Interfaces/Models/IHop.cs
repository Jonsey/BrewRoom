namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IHop
    {
        decimal AlphaAcid { get; }
        string Name { get; }

        void AddOilCharacteristics(IHopOilCharacteristics hopOilCharacteristics);
        IHopOilCharacteristics GetCharacteristics();
        decimal GetAlphaAcid();
    }
}