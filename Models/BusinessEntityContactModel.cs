using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class BusinessEntityContactModel {
        
        protected internal int _businessEntityID;
        
        protected internal int _personID;
        
        protected internal int _contactTypeID;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public BusinessEntityContactModel() {
        }
        
        internal BusinessEntityContactModel(BusinessEntityContact entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._personID = entity.PersonID;
            this._contactTypeID = entity.ContactTypeID;
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
        [Display(Name = "Person ID")]
        public int PersonID {
            get {
                return this._personID;
            }
            set {
                this._personID = value;
            }
        }
        
        [Required()]
        [Display(Name = "Contact type ID")]
        public int ContactTypeID {
            get {
                return this._contactTypeID;
            }
            set {
                this._contactTypeID = value;
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
            hash ^=PersonID.GetHashCode();
            hash ^=ContactTypeID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
                 + "-" + PersonID.ToString()
                 + "-" + ContactTypeID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is BusinessEntityContactModel) {
                BusinessEntityContactModel toCompare = (BusinessEntityContactModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(BusinessEntityContactModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && toCompare.PersonID == PersonID
             && toCompare.ContactTypeID == ContactTypeID
;
            }

            return result;
        }
    }
}

