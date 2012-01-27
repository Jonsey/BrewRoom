using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Interfaces.ViewModels
{
    public interface IRecipeHopViewModel : IHopViewModel
    {
        Weight Weight  { get; set; }
        int BoilTime { get; set; }
    }
}