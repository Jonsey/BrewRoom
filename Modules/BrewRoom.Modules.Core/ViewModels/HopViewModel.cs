using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Models;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class HopViewModel : IHopViewModel
    {
        readonly IHop hop;
        IHopOilCharacteristics hopOilCharacteristics;

        public HopViewModel(IHop hop)
        {
            this.hop = hop;
            hopOilCharacteristics = this.hop.GetCharacteristics();
        }

        public IHop Model
        {
            get { return hop; }
        }

        public String Name
        {
            get
            {
                return hop.Name;
            }
            set
            {
                hop.Name = value;
            }
        }

        public string Description
        {
            get { return hop.Description; }
            set { hop.Description = value; }
        }

        public decimal PercentageOfTotalWeight
        {
            get
            {
                return hopOilCharacteristics.PercentageOfTotalWeight;
            }
            set
            {
                hopOilCharacteristics.PercentageOfTotalWeight = value;
            }
        }

        public decimal Farnesene
        {
            get
            {
                return hopOilCharacteristics.Farnesene;
            }
            set
            {
                hopOilCharacteristics.Farnesene = value;
            }
        }

        public decimal Carophyllene
        {
            get
            {
                return hopOilCharacteristics.Carophyllene;
            }
            set
            {
                hopOilCharacteristics.Carophyllene = value;
            }
        }

        public decimal Myrcene
        {
            get
            {
                return hopOilCharacteristics.Myrcene;
            }
            set
            {
                hopOilCharacteristics.Myrcene = value;
            }
        }

        public decimal Humulene
        {
            get
            {
                return hopOilCharacteristics.Humulene;
            }
            set
            {
                hopOilCharacteristics.Humulene = value;
            }
        }

        public decimal OtherAcids
        {
            get
            {
                return hopOilCharacteristics.OtherAcids;
            }
            set
            {
                hopOilCharacteristics.OtherAcids = value;
            }
        }

        public decimal AlphaAcid
        {
            get
            {
                return Math.Round(hopOilCharacteristics.TotalAlphaAcid, 1);
            }
            set
            {
                hopOilCharacteristics.TotalAlphaAcid = value;
            }
        }

        #region Equality Members

        #endregion
    }
}
