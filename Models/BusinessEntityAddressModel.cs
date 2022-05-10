// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class BusinessEntityAddressModel {
        
        protected internal int _businessEntityID;
        
        protected internal int _addressID;
        
        protected internal int _addressTypeID;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public BusinessEntityAddressModel() {
        }
        
        internal BusinessEntityAddressModel(BusinessEntityAddress entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._addressID = entity.AddressID;
            this._addressTypeID = entity.AddressTypeID;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Required()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID {
            get {
                return this._businessEntityID;
            }
            set {
                this._businessEntityID = value;
            }
        }
        
        [Required()]
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
        [Display(Name = "Address type ID")]
        public int AddressTypeID {
            get {
                return this._addressTypeID;
            }
            set {
                this._addressTypeID = value;
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
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=BusinessEntityID.GetHashCode();
            hash ^=AddressID.GetHashCode();
            hash ^=AddressTypeID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
                 + "-" + AddressID.ToString()
                 + "-" + AddressTypeID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is BusinessEntityAddressModel) {
                BusinessEntityAddressModel toCompare = (BusinessEntityAddressModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(BusinessEntityAddressModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && toCompare.AddressID == AddressID
             && toCompare.AddressTypeID == AddressTypeID
;
            }

            return result;
        }
    }
}
