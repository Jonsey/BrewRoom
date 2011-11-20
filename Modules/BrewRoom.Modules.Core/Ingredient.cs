using System;

namespace BrewRoom.Modules.Core
{
    public abstract class Ingredient
    {
        public String Name { get; protected set; }

        protected Ingredient(String name)
        {
            Name = name;
        }
    }
}