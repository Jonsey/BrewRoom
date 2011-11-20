namespace BrewRoom.Modules.Core.ValueObjects
{
    public class Weight
    {
        #region Fields
        private readonly decimal _value;
        private readonly MassUnit _unit;
        #endregion

        public Weight(decimal value, MassUnit unit)
        {
            _value = value;
            _unit = unit;
        }

        #region Operator overloads

        public static Weight operator +(Weight weight1, Weight weight2)
        {
            return new Weight(weight1._value + weight2._value, weight1._unit);
        }

        public static Weight operator *(Weight weight1, dynamic multiplier)
        {
            return new Weight(weight1._value + multiplier, weight1._unit);
        }

        #endregion

        #region Equality Members
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(Weight)) return false;
            return Equals((Weight)obj);
        }

        public bool Equals(Weight other)
        {
            return other._value.Equals(_value) && Equals(other._unit, _unit);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (_value.GetHashCode() * 397) ^ _unit.GetHashCode();
            }
        }

        public static bool operator ==(Weight left, Weight right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Weight left, Weight right)
        {
            return !Equals(left, right);
        }
        #endregion

        public Weight ConvertTo(MassUnit unit)
        {
            switch (_unit)
            {
                case MassUnit.Grams:
                    {
                        return FromGrams(unit);
                    }
                case MassUnit.KiloGrams:
                    {
                        return FromKiloGrams(unit);
                    }
                case MassUnit.Pounds:
                    {
                        return FromPounds(unit);
                    }
            }
            return null;
        }

        private Weight FromPounds(MassUnit unit)
        {
            switch (unit)
            {
                case MassUnit.Grams:
                    {
                        return new Weight(_value * 453.592M, unit);
                    }
                case MassUnit.KiloGrams:
                    {
                        return new Weight(_value / 1000M * 453.592M, unit);
                    }
                case MassUnit.Pounds:
                    {
                        return new Weight(_value, unit);
                    }
            }
            return null;
        }

        private Weight FromKiloGrams(MassUnit unit)
        {
            switch (unit)
            {
                case MassUnit.Grams:
                    {
                        return new Weight(_value * 1000M, unit);
                    }
                case MassUnit.KiloGrams:
                    {
                        return new Weight(_value, unit);
                    }
                case MassUnit.Pounds:
                    {
                        return new Weight(_value * 2.20462M, unit);
                    }
            }
            return null;
        }

        private Weight FromGrams(MassUnit unit)
        {
            switch (unit)
            {
                case MassUnit.Grams:
                    {
                        return new Weight(_value, unit);
                    }
                case MassUnit.KiloGrams:
                    {
                        return new Weight(_value / 1000, unit);
                    }
                case MassUnit.Pounds:
                    {
                        return new Weight(_value / 1000 * 2.20462M, unit);
                    }
            }

            return null;
        }

        public decimal GetValue()
        {
            return _value;
        }
    }
}