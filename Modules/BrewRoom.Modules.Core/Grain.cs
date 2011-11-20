using System;

namespace BrewRoom.Modules.Core
{
    public class Grain : Ingredient
    {
        public decimal PotentialGravity { get; protected set; }

        public Grain(string name)
            : base(name)
        {

        }
    }
}
