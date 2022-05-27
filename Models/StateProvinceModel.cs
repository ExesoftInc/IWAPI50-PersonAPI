using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class StateProvinceModel {
        
        protected internal int _stateProvinceID;
        
        protected internal string _stateProvinceCode;
        
        protected internal string _countryRegionCode;
        
        protected internal bool _isOnlyStateProvinceFlag;
        
        protected internal string _name;
        
        protected internal int _territoryID;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public StateProvinceModel() {
        }
        
        internal StateProvinceModel(StateProvince entity) {
            this._stateProvinceID = entity.StateProvinceID;
            this._stateProvinceCode = entity.StateProvinceCode;
            this._countryRegionCode = entity.CountryRegionCode;
            this._isOnlyStateProvinceFlag = entity.IsOnlyStateProvinceFlag;
            this._name = entity.Name;
            this._territoryID = entity.TerritoryID;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "State province ID")]
        public int StateProvinceID {
            get {
                return this._stateProvinceID;
            }
            set {
                this._stateProvinceID = value;
            }
        }
        
        [Required()]
        [MaxLength(3)]
        [StringLength(3)]
        [Display(Name = "State province code")]
        public string StateProvinceCode {
            get {
                return this._stateProvinceCode;
            }
            set {
                this._stateProvinceCode = value;
            }
        }
        
        [Required()]
        [MaxLength(3)]
        [StringLength(3)]
        [Display(Name = "Country region code")]
        public string CountryRegionCode {
            get {
                return this._countryRegionCode;
            }
            set {
                this._countryRegionCode = value;
            }
        }
        
        [Required()]
        [Display(Name = "Is only state province flag")]
        public bool IsOnlyStateProvinceFlag {
            get {
                return this._isOnlyStateProvinceFlag;
            }
            set {
                this._isOnlyStateProvinceFlag = value;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Name")]
        public string Name {
            get {
                return this._name;
            }
            set {
                this._name = value;
            }
        }
        
        [Required()]
        [Display(Name = "Territory ID")]
        public int TerritoryID {
            get {
                return this._territoryID;
            }
            set {
                this._territoryID = value;
            }
        }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid {
            get {
                return this._rowguid;
            }
        }
        
        [Required()]
        [DataType(DataType.DateTime)]
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate {
            get {
                return this._modifiedDate;
            }
            set {
                this._modifiedDate = value;
            }
        }
        
        /// Child Addresses where [Address].[StateProvinceID] point to this entity (FK_Address_StateProvince_StateProvinceID)
        public virtual ICollection<AddressModel> AddressesModel { get; set; } = new HashSet<AddressModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=StateProvinceCode.GetHashCode();
            hash ^=CountryRegionCode.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return StateProvinceCode
                 + "-" + CountryRegionCode
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is StateProvinceModel) {
                StateProvinceModel toCompare = (StateProvinceModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(StateProvinceModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.StateProvinceCode, StateProvinceCode, true) == 0
             && string.Compare(toCompare.CountryRegionCode, CountryRegionCode, true) == 0
;
            }

            return result;
        }
    }
}

