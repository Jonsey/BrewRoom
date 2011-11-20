using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrewRoom.Modules.Core.ValueObjects
{
    public static class WeightExtensions
    {
        public static Weight KiloGrams(this int value)
        {
            return new Weight(value, MassUnit.KiloGrams);
        }

        public static Weight KiloGram(this int value)
        {
            return new Weight(value, MassUnit.KiloGrams);
        }

        public static Weight Pounds(this int value)
        {
            return new Weight(value, MassUnit.Pounds);
        }

        public static Weight Pound(this int value)
        {
            return new Weight(value, MassUnit.Pounds);
        }
    }

    public static class VoloumeExtensions
    {
        public static Volume Litre(this int value)
        {
            return new Volume(value, VolumeUnit.Litres);
        }

        public static Volume Litres(this int value)
        {
            return new Volume(value, VolumeUnit.Litres);
        }

        public static Volume Gallon(this int value)
        {
            return new Volume(value, VolumeUnit.Gallons);
        }

        public static Volume Gallons(this int value)
        {
            return new Volume(value, VolumeUnit.Gallons);
        }
    }

 
}
