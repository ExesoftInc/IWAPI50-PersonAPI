using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class CountryRegionModel {
        
        protected internal string _countryRegionCode;
        
        protected internal string _name;
        
        protected internal System.DateTime _modifiedDate;
        
        public CountryRegionModel() {
        }
        
        internal CountryRegionModel(CountryRegion entity) {
            this._countryRegionCode = entity.CountryRegionCode;
            this._name = entity.Name;
            this._modifiedDate = entity.ModifiedDate;
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
        
        /// Child StateProvinces where [StateProvince].[CountryRegionCode] point to this entity (FK_StateProvince_CountryRegion_CountryRegionCode)
        public virtual ICollection<StateProvinceModel> StateProvincesModel { get; set; } = new HashSet<StateProvinceModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=CountryRegionCode.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return CountryRegionCode
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is CountryRegionModel) {
                CountryRegionModel toCompare = (CountryRegionModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(CountryRegionModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.CountryRegionCode, CountryRegionCode, true) == 0
;
            }

            return result;
        }
    }
}

