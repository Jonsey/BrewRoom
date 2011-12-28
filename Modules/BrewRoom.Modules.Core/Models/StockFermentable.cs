using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public class StockFermentable : Fermentable, IStockFermentable
    {
        public StockFermentable(string name, decimal pppg) : base(name, pppg)
        {
        }
    }
}
