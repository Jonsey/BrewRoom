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

        public HopViewModel(IHop hop)
        {
            this.hop = hop;
            name = hop.Name;
            alphaAcid = hop.AlphaAcid;
        }


        String name;
        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        decimal alphaAcid;
        public decimal AlphaAcid
        {
            get { return alphaAcid; }
            set { alphaAcid = value; }
        }

        #region Equality Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(HopViewModel)) return false;
            return Equals((HopViewModel)obj);
        }

        public bool Equals(HopViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.name, name);
        }

        public override int GetHashCode()
        {
            return (name != null ? name.GetHashCode() : 0);
        }

        public static bool operator ==(HopViewModel left, HopViewModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HopViewModel left, HopViewModel right)
        {
            return !Equals(left, right);
        } 
        #endregion
    }
}
