using BrewRoom.Modules.Core.ValueObjects;

namespace BrewRoom.Modules.Core
{
    public class RecipeGrain
    {
        private readonly Grain _grain;
        private readonly Weight _weight;
        private readonly decimal _pppg;

        public RecipeGrain(Grain grain, Weight weight)
        {
            _grain = grain;
            _weight = weight;
        }

        public RecipeGrain(Grain grain, Weight weight, decimal pppg)
        {
            _grain = grain;
            _weight = weight;
            _pppg = pppg;
        }

        public Weight GetWeight()
        {
            return _weight;
        }

        public decimal GetExtractPoints()
        {
            return (_pppg - 1) * 1000;
        }
    }
}