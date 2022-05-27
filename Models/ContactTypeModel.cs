using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class ContactTypeModel {
        
        protected internal int _contactTypeID;
        
        protected internal string _name;
        
        protected internal System.DateTime _modifiedDate;
        
        public ContactTypeModel() {
        }
        
        internal ContactTypeModel(ContactType entity) {
            this._contactTypeID = entity.ContactTypeID;
            this._name = entity.Name;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Contact type ID")]
        public int ContactTypeID {
            get {
                return this._contactTypeID;
            }
            set {
                this._contactTypeID = value;
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
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[ContactTypeID] point to this entity (FK_BusinessEntityContact_ContactType_ContactTypeID)
        public virtual ICollection<BusinessEntityContactModel> BusinessEntityContactsModel { get; set; } = new HashSet<BusinessEntityContactModel>();
        
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

            if (obj is ContactTypeModel) {
                ContactTypeModel toCompare = (ContactTypeModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(ContactTypeModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = string.Compare(toCompare.Name, Name, true) == 0
;
            }

            return result;
        }
    }
}

