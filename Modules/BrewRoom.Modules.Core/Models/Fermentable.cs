using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public class Fermentable : Ingredient, IFermentable
    {
        protected decimal pppg;

        public virtual decimal Pppg
        {
            get { return pppg; }
            protected set { pppg = value; }
        }

        public virtual decimal ExtractPoints
        {
            get
            {
                return (pppg - 1) * 1000;
            }
        }

        protected Fermentable()
        {
        }

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
