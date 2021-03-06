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
    
    
    public class PersonPhoneModel {
        
        protected internal int _businessEntityID;
        
        protected internal string _phoneNumber;
        
        protected internal int _phoneNumberTypeID;
        
        protected internal System.DateTime _modifiedDate;
        
        public PersonPhoneModel() {
        }
        
        internal PersonPhoneModel(PersonPhone entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._phoneNumber = entity.PhoneNumber;
            this._phoneNumberTypeID = entity.PhoneNumberTypeID;
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
        [MaxLength(25)]
        [StringLength(25)]
        [Phone()]
        [Display(Name = "Phone number")]
        public string PhoneNumber {
            get {
                return this._phoneNumber;
            }
            set {
                this._phoneNumber = value;
            }
        }
        
        [Required()]
        [Display(Name = "Phone number type ID")]
        public int PhoneNumberTypeID {
            get {
                return this._phoneNumberTypeID;
            }
            set {
                this._phoneNumberTypeID = value;
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
            hash ^=PhoneNumber.GetHashCode();
            hash ^=PhoneNumberTypeID.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return BusinessEntityID.ToString()
                 + "-" + PhoneNumber
                 + "-" + PhoneNumberTypeID.ToString()
;
        }
        
        public override bool Equals(object obj) {
        bool result = false;

            if (obj is PersonPhoneModel) {
                PersonPhoneModel toCompare = (PersonPhoneModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(PersonPhoneModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
             && string.Compare(toCompare.PhoneNumber, PhoneNumber, true) == 0
             && toCompare.PhoneNumberTypeID == PhoneNumberTypeID
;
            }

            return result;
        }
    }
}

