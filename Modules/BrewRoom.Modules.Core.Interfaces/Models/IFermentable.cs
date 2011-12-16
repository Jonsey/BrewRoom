using System;

namespace BrewRoom.Modules.Core.Interfaces.Models
{
    public interface IFermentable
    {
        string Name { get; }
        decimal Pppg { get; }
        Guid Id { get; }
    }
}