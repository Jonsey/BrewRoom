using System;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IFermentable
    {
        Guid Id { get; }
        string Name { get; set; }
        decimal Pppg { get; set; }
        decimal ExtractPoints { get; }
        string Description { get; set; }
    }
}