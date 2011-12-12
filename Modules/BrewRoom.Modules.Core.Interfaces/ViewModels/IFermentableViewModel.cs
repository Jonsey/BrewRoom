using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IFermentableViewModel : IIngredientViewModel
    {
        decimal Pppg { get; set; }
        IFermentable Model { get; }
    }
}