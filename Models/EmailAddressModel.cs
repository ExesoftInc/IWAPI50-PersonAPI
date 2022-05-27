using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class EmailAddressModel {
        
        protected internal int _businessEntityID;
        
        protected internal int _emailAddressID;
        
        protected internal string _emailAddress_;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public EmailAddressModel() {
        }
        
        internal EmailAddressModel(EmailAddress entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._emailAddressID = entity.EmailAddressID;
            this._emailAddress_ = entity.EmailAddress_;
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
        
        [Display(Name = "Email address ID")]
        public int EmailAddressID {
            get {
                return this._emailAddressID;
            }
            set {
                this._emailAddressID = value;
            }
        }
        
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Email address")]
        public string EmailAddress_ {
            get {
                return this._emailAddress_;
            }
            set {
                this._emailAddress_ = value;
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
            hash ^=Rowguid.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is EmailAddressModel) {
                EmailAddressModel toCompare = (EmailAddressModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(EmailAddressModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && toCompare.EmailAddress_.Equals(EmailAddress_)
;
            }

            return result;
        }
    }
}

