using System;
using Zymurgy.Dymensions;

namespace BrewRoom.Modules.Core.Calculators
{
    public class MashCalculator
    {
        #region Private Fields
        private decimal grainAmount;
        private decimal liquorAmount;
        private decimal ratio; 
        #endregion

        #region Ctors

        public MashCalculator()
        {

        }

        public MashCalculator(decimal grainAmount, decimal liquorAmount)
        {
            this.grainAmount = grainAmount;
            this.liquorAmount = liquorAmount;
            ratio = CalculateRatio();
        }

        #endregion
        
        #region Public Methods

        public decimal GetStrikeTemp(decimal desiredStrikeTemp, decimal grainTemp, decimal ratio, Weight grainAmount)
        {
            var tw = (0.2M / ratio) * (desiredStrikeTemp - grainTemp) + desiredStrikeTemp;

            liquorAmount = (grainAmount.ConvertTo(MassUnit.KiloGrams).GetValue() / ratio);
            this.grainAmount = grainAmount.ConvertTo(MassUnit.KiloGrams).GetValue();

            return tw;
        }

        public decimal GetInfusionAmount(Weight grain, decimal desiredInfusionTemp, decimal liquorTemperature, decimal strikeTemp)
        {
            ReCalcRatio();

            decimal grainWeight = grain.ConvertTo(MassUnit.Pounds).GetValue();
            var wm = ratio * grainWeight;


            var wa = (desiredInfusionTemp - strikeTemp) * ((0.2M * grainWeight) + wm) /
                     (liquorTemperature - desiredInfusionTemp);

            liquorAmount += wa;

            ReCalcRatio();

            return Math.Round(wa);
        }

        public decimal GetInfusionAmount(decimal ratio, Weight grain, decimal desiredStepTemp, decimal liquorTemperature, decimal previousStepTemp)
        {
            decimal grainWeight = grain.ConvertTo(MassUnit.Pounds).GetValue();
            var wm = ratio * grainWeight;


            var wa = (desiredStepTemp - previousStepTemp) * ((0.2M * grainWeight) + wm) /
                     (liquorTemperature - desiredStepTemp);

            return Math.Round(wa);
        }

        #endregion

        #region Private Methods

        private decimal CalculateRatio()
        {
            return grainAmount / liquorAmount;
        }

        private void ReCalcRatio()
        {
            ratio = CalculateRatio();
        } 

        #endregion
    }
}