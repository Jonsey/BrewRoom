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
            get { return _fermentable.Name; }
            set { _fermentable.Name = value; }
        }

        decimal _pppg;

        public decimal Pppg
        {
            get { return _fermentable.Pppg; }
            set { _fermentable.Pppg = value; }
        }

        public IFermentable Model
        {
            get { return _fermentable; }
        }

        private string _description;
        public string Description
        {
            get { return _fermentable.Description; }
            set { _fermentable.Description = value; }
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
            return Equals(other.Name, _fermentable.Name) && other.Pppg == _fermentable.Pppg;
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
                return ((_fermentable.Name != null ? _fermentable.Name.GetHashCode() : 0) * 397) ^ _fermentable.Pppg.GetHashCode();
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
