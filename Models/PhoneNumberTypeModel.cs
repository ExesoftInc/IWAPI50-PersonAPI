using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class PhoneNumberTypeModel {
        
        protected internal int _phoneNumberTypeID;
        
        protected internal string _name;
        
        protected internal System.DateTime _modifiedDate;
        
        public PhoneNumberTypeModel() {
        }
        
        internal PhoneNumberTypeModel(PhoneNumberType entity) {
            this._phoneNumberTypeID = entity.PhoneNumberTypeID;
            this._name = entity.Name;
            this._modifiedDate = entity.ModifiedDate;
        }
        
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
        
        /// Child PersonPhones where [PersonPhone].[PhoneNumberTypeID] point to this entity (FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID)
        public virtual ICollection<PersonPhoneModel> PersonPhonesModel { get; set; } = new HashSet<PersonPhoneModel>();
        
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

            if (obj is PhoneNumberTypeModel) {
                PhoneNumberTypeModel toCompare = (PhoneNumberTypeModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(PhoneNumberTypeModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.Name, Name, true) == 0
;
            }

            return result;
        }
    }
}

