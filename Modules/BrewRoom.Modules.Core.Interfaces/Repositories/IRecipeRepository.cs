using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;

namespace BrewRoom.Modules.Core.Interfaces.Repositories
{
    public interface IRecipeRepository
    {
        Guid Save(IRecipe recipe);
    }
}
