using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class PasswordModel {
        
        protected internal int _businessEntityID;
        
        protected internal string _passwordHash;
        
        protected internal string _passwordSalt;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public PasswordModel() {
        }
        
        internal PasswordModel(Password entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._passwordHash = entity.PasswordHash;
            this._passwordSalt = entity.PasswordSalt;
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
        [MaxLength(128)]
        [StringLength(128)]
        [Display(Name = "Password hash")]
        public string PasswordHash {
            get {
                return this._passwordHash;
            }
            set {
                this._passwordHash = value;
            }
        }
        
        [Required()]
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Password salt")]
        public string PasswordSalt {
            get {
                return this._passwordSalt;
            }
            set {
                this._passwordSalt = value;
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
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is PasswordModel) {
                PasswordModel toCompare = (PasswordModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(PasswordModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
;
            }

            return result;
        }
    }
}

