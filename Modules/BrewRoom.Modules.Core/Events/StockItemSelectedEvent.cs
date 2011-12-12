using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using Microsoft.Practices.Prism.Events;

namespace BrewRoom.Modules.Core.Events
{
    public class StockItemSelectedEvent : CompositePresentationEvent<IIngredientViewModel>
    {
    }
}
