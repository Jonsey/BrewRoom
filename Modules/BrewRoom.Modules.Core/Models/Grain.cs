namespace BrewRoom.Modules.Core.Models
{
    public class Grain : Ingredient
    {
        public decimal Pppg { get; protected set; }

        public Grain(string name)
            : base(name)
        {

        }

        public Grain(string name, decimal pppg) : base(name)
        {
            Pppg = pppg;
        }
    }
}
