using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public class Fermentable : Ingredient, IFermentable
    {
        public decimal Pppg { get; protected set; }

        public Fermentable(string name)
            : base(name)
        {

        }

        public Fermentable(string name, decimal pppg) : base(name)
        {
            Pppg = pppg;
        }
    }
}
