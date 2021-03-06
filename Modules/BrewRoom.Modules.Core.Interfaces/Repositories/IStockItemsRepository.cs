using System;
using System.Collections.Generic;
using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Interfaces.Repositories
{
    public interface IStockItemsRepository
    {
        IEnumerable<IFermentable> GetStockFermentables();
        IEnumerable<IHop> GetHops();
        Guid Save(IFermentable fermentable);
        Guid Save(IHop hop);
    }
}