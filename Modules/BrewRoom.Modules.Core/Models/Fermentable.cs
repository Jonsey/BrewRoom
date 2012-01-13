using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Models
{
    public abstract class Fermentable : Ingredient, IFermentable
    {
        protected decimal pppg;

        public virtual decimal Pppg
        {
            get { return pppg; }
            set { pppg = value; }
        }

        public virtual decimal ExtractPoints
        {
            get
            {
                return (pppg - 1) * 1000;
            }
        }

        public virtual string Description { get; set; }

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
