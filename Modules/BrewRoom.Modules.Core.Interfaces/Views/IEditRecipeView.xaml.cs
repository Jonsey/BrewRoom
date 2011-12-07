using BrewRoom.Modules.Core.Interfaces.ViewModels;

namespace BrewRoom.Modules.Core.Interfaces.Views
{
    public interface IEditRecipeView
    {
        IEditRecipeViewModel ViewModel { get; set; }
    }
}