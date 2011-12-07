using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IHopViewModel
    {
        String Name  { get; set; }
        decimal AlphaAcid { get; set; }
    }
}
