using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrewRoom.Modules.Core.Interfaces.Models;
using BrewRoom.Modules.Core.Interfaces.ViewModels;
using BrewRoom.Modules.Core.Models;

namespace BrewRoom.Modules.Core.ViewModels
{
    public class  FermentableViewModel : IFermentableViewModel
    {
        #region Fields
        readonly IFermentable _fermentable; 
        #endregion

        #region Ctors
        public FermentableViewModel()
        {
            this._fermentable = new Fermentable("");
        }

        public FermentableViewModel(IFermentable fermentable)
        {
            _fermentable = fermentable;
            _name = fermentable.Name;
            _pppg = fermentable.Pppg;
        } 
        #endregion

        #region Properties

        string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        decimal _pppg;

        public decimal Pppg
        {
            get { return _pppg; }
            set { _pppg = value; }
        }

        public IFermentable Model
        {
            get { return _fermentable; }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
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
            return Equals(other._name, _name) && other._pppg == _pppg;
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
                return ((_name != null ? _name.GetHashCode() : 0) * 397) ^ _pppg.GetHashCode();
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
