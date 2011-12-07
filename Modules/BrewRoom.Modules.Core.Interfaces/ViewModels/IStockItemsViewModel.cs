using System.Collections.Generic;
using Microsoft.Practices.Prism.Commands;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IStockItemsViewModel
    {
        IList<IFermentableViewModel> Fermentables { get; }
        IList<IHopViewModel> Hops { get; }

        IFermentableViewModel SelectedFermentable { get; set; }
        IHopViewModel SelectedHop { get; set; }

        bool IsHopDetailsVisible { get; }
        bool IsFermentableDetailsVisible { get; }
        
        DelegateCommand SelectHops { get; }
        DelegateCommand SelectFermentables { get; }
        
    }
}