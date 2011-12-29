using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.ViewModels;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class StockHopViewModel : HopViewModel, IStockHopViewModel
    {
        public StockHopViewModel(IHop hop) : base(hop)
        {
        }
    }
}
