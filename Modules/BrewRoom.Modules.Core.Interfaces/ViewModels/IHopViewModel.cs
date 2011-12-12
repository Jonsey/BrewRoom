using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IHopViewModel : IIngredientViewModel
    {
        decimal AlphaAcid { get; set; }
        IHop Model { get; }
    }
}
