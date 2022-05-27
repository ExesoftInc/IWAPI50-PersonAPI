using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class AddressTypeModel {
        
        protected internal int _addressTypeID;
        
        protected internal string _name;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public AddressTypeModel() {
        }
        
        internal AddressTypeModel(AddressType entity) {
            this._addressTypeID = entity.AddressTypeID;
            this._name = entity.Name;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Address type ID")]
        public int AddressTypeID {
            get {
                return this._addressTypeID;
            }
            set {
                this._addressTypeID = value;
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
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[AddressTypeID] point to this entity (FK_BusinessEntityAddress_AddressType_AddressTypeID)
        public virtual ICollection<BusinessEntityAddressModel> BusinessEntityAddressesModel { get; set; } = new HashSet<BusinessEntityAddressModel>();
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=Name.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return Name
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is AddressTypeModel) {
                AddressTypeModel toCompare = (AddressTypeModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(AddressTypeModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.Name, Name, true) == 0
;
            }

            return result;
        }
    }
}

