using System;

namespace BrewRoom.Modules.Core.Models
{
    public abstract class Ingredient : EntityBase
    {
        public virtual String Name { get; set; }

        protected Ingredient()
        {
        }

        protected Ingredient(String name)
        {
            Name = name;
        }
    }
}