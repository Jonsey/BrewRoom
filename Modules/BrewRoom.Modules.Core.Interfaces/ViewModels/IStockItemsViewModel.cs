using System.Collections.Generic;
using Microsoft.Practices.Prism.Commands;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IStockItemsViewModel
    {
        IList<IFermentableViewModel> Fermentables { get; }
        IList<IHopViewModel> Hops { get; }

        bool IsHopDetailsVisible { get; }
        bool IsFermentableDetailsVisible { get; }
        
        DelegateCommand SelectHops { get; }
        DelegateCommand SelectFermentables { get; }
        DelegateCommand SaveFermentableCommand { get; }

        IStockFermentableViewModel SelectedFermentable { get; set; }
        IStockHopViewModel SelectedHop { get; set; }
    }
}