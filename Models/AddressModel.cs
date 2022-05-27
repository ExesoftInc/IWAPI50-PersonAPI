using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class AddressModel {
        
        protected internal int _addressID;
        
        protected internal string _addressLine1;
        
        protected internal string _addressLine2;
        
        protected internal string _city;
        
        protected internal int _stateProvinceID;
        
        protected internal string _postalCode;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public AddressModel() {
        }
        
        internal AddressModel(Address entity) {
            this._addressID = entity.AddressID;
            this._addressLine1 = entity.AddressLine1;
            this._addressLine2 = entity.AddressLine2;
            this._city = entity.City;
            this._stateProvinceID = entity.StateProvinceID;
            this._postalCode = entity.PostalCode;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Address ID")]
        public int AddressID {
            get {
                return this._addressID;
            }
            set {
                this._addressID = value;
            }
        }
        
        [Required()]
        [MaxLength(60)]
        [StringLength(60)]
        [Display(Name = "Address line 1")]
        public string AddressLine1 {
            get {
                return this._addressLine1;
            }
            set {
                this._addressLine1 = value;
            }
        }
        
        [MaxLength(60)]
        [StringLength(60)]
        [Display(Name = "Address line 2")]
        public string AddressLine2 {
            get {
                return this._addressLine2;
            }
            set {
                this._addressLine2 = value;
            }
        }
        
        [Required()]
        [MaxLength(30)]
        [StringLength(30)]
        [Display(Name = "City")]
        public string City {
            get {
                return this._city;
            }
            set {
                this._city = value;
            }
        }
        
        [Required()]
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
        [MaxLength(15)]
        [StringLength(15)]
        [DataType(DataType.PostalCode)]
        [Display(Name = "Postal code")]
        public string PostalCode {
            get {
                return this._postalCode;
            }
            set {
                this._postalCode = value;
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
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[AddressID] point to this entity (FK_BusinessEntityAddress_Address_AddressID)
        public virtual ICollection<BusinessEntityAddressModel> BusinessEntityAddressesModel { get; set; } = new HashSet<BusinessEntityAddressModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=AddressLine1.GetHashCode();
            hash ^=City.GetHashCode();
            hash ^=StateProvinceID.GetHashCode();
            hash ^=PostalCode.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return AddressLine1
                 + "-" + City
                 + "-" + StateProvinceID.ToString()
                 + "-" + PostalCode
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is AddressModel) {
                AddressModel toCompare = (AddressModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(AddressModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.AddressLine1, AddressLine1, true) == 0
             && string.Compare(toCompare.City, City, true) == 0
             && toCompare.StateProvinceID == StateProvinceID
             && string.Compare(toCompare.PostalCode, PostalCode, true) == 0
;
            }

            return result;
        }
    }
}

