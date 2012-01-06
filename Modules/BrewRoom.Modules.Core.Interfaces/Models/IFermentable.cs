using System;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IFermentable
    {
        string Name { get; set; }
        decimal Pppg { get; set; }
        Guid Id { get; }
        decimal ExtractPoints { get; }
        string Description { get; set; }
    }
}