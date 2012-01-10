using BrewRoom.Modules.Core.Interfaces.ViewModels.Admin;

namespace BrewRoom.Modules.Core.Interfaces.Views
{
    public interface IAdminView
    {
        IStockItemInfoViewModel ViewModel { get; set; }
    }
}