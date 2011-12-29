using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.ViewModels;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class  FermentableViewModel : IFermentableViewModel
    {
        #region Fields
        readonly IFermentable fermentable; 
        #endregion

        #region Ctors
        public FermentableViewModel(IFermentable fermentable)
        {
            this.fermentable = fermentable;
            name = fermentable.Name;
            pppg = fermentable.Pppg;
        } 
        #endregion

        #region Properties

        string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        decimal pppg;
        public decimal Pppg
        {
            get { return pppg; }
            set { pppg = value; }
        }

        public IFermentable Model
        {
            get { return fermentable; }
        }

        #endregion

        #region Equality Members
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(FermentableViewModel)) return false;
            return Equals((FermentableViewModel)obj);
        }

        public bool Equals(FermentableViewModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.name, name) && other.pppg == pppg;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            unchecked
            {
                return ((name != null ? name.GetHashCode() : 0) * 397) ^ pppg.GetHashCode();
            }
        }

        public static bool operator ==(FermentableViewModel left, FermentableViewModel right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(FermentableViewModel left, FermentableViewModel right)
        {
            return !Equals(left, right);
        } 
        #endregion
    }
}
