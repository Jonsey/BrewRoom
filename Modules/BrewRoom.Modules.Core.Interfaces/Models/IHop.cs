using System;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IHop
    {
        Guid Id { get; }

        string Name { get; set; }
        string Description { get; set; }

        void AddOilCharacteristics(IHopOilCharacteristics hopOilCharacteristics);
        IHopOilCharacteristics GetCharacteristics();
        decimal GetAlphaAcid();
    }
}