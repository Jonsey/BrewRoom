﻿using System;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.ViewModels;

namespace BrewRoom.Modules.Core.Models
{
    public class Hop : Ingredient, IHop
    {
        private IHopOilCharacteristics characteristics;

        #region Properties
        public virtual IHopOilCharacteristics Characteristics
        {
            get { return characteristics; }
            private set { characteristics = value; }
        }

        #endregion

        #region Ctor
        protected Hop()
        {
        }

        public Hop(string name)
            : base(name)
        {
        }

        #endregion

        public virtual void AddOilCharacteristics(IHopOilCharacteristics hopCharacteristics)
        {
            if (!hopCharacteristics.AreValid())
                throw new ArgumentException("Characteristics are invalid. See innerexception for details.");
            
            this.characteristics = hopCharacteristics;
        }

        public virtual IHopOilCharacteristics GetCharacteristics()
        {
            return characteristics;
        }

        public virtual decimal GetAlphaAcid()
        {
            return characteristics.TotalAlphaAcid;
        }
    }
}
