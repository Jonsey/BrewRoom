using System;

namespace BrewRoom.Modules.Core.Models
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