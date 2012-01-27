using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Models
{
    public class StockHop : Hop
    {
        public virtual Recipe Recipe { get; private set; }

        public virtual Weight Weight { get; set; }
    }
}
