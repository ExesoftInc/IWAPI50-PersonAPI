using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class PersonModel {
        
        protected internal int _businessEntityID;
        
        protected internal string _personType;
        
        protected internal bool _nameStyle;
        
        protected internal string _title;
        
        protected internal string _firstName;
        
        protected internal string _middleName;
        
        protected internal string _lastName;
        
        protected internal string _suffix;
        
        protected internal int _emailPromotion;
        
        protected internal string _additionalContactInfo;
        
        protected internal string _demographics;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public PersonModel() {
        }
        
        internal PersonModel(Person entity) {
            this._businessEntityID = entity.BusinessEntityID;
            this._personType = entity.PersonType;
            this._nameStyle = entity.NameStyle;
            this._title = entity.Title;
            this._firstName = entity.FirstName;
            this._middleName = entity.MiddleName;
            this._lastName = entity.LastName;
            this._suffix = entity.Suffix;
            this._emailPromotion = entity.EmailPromotion;
            this._additionalContactInfo = entity.AdditionalContactInfo;
            this._demographics = entity.Demographics;
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
        [MaxLength(2)]
        [StringLength(2)]
        [Display(Name = "Person type")]
        public string PersonType {
            get {
                return this._personType;
            }
            set {
                this._personType = value;
            }
        }
        
        [Required()]
        [Display(Name = "Name style")]
        public bool NameStyle {
            get {
                return this._nameStyle;
            }
            set {
                this._nameStyle = value;
            }
        }
        
        [MaxLength(8)]
        [StringLength(8)]
        [Display(Name = "Title")]
        public string Title {
            get {
                return this._title;
            }
            set {
                this._title = value;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "First name")]
        public string FirstName {
            get {
                return this._firstName;
            }
            set {
                this._firstName = value;
            }
        }
        
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Middle name")]
        public string MiddleName {
            get {
                return this._middleName;
            }
            set {
                this._middleName = value;
            }
        }
        
        [Required()]
        [MaxLength(50)]
        [StringLength(50)]
        [Display(Name = "Last name")]
        public string LastName {
            get {
                return this._lastName;
            }
            set {
                this._lastName = value;
            }
        }
        
        [MaxLength(10)]
        [StringLength(10)]
        [Display(Name = "Suffix")]
        public string Suffix {
            get {
                return this._suffix;
            }
            set {
                this._suffix = value;
            }
        }
        
        [Required()]
        [Display(Name = "Email promotion")]
        public int EmailPromotion {
            get {
                return this._emailPromotion;
            }
            set {
                this._emailPromotion = value;
            }
        }
        
        [Display(Name = "Additional contact info")]
        public string AdditionalContactInfo {
            get {
                return this._additionalContactInfo;
            }
            set {
                this._additionalContactInfo = value;
            }
        }
        
        [Display(Name = "Demographics")]
        public string Demographics {
            get {
                return this._demographics;
            }
            set {
                this._demographics = value;
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
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[PersonID] point to this entity (FK_BusinessEntityContact_Person_PersonID)
        public virtual ICollection<BusinessEntityContactModel> BusinessEntityContactsModel { get; set; } = new HashSet<BusinessEntityContactModel>();
        
        /// Child EmailAddresses where [EmailAddress].[BusinessEntityID] point to this entity (FK_EmailAddress_Person_BusinessEntityID)
        public virtual ICollection<EmailAddressModel> EmailAddressesModel { get; set; } = new HashSet<EmailAddressModel>();
        
        /// Parent (One-to-One) Person pointed by [Password].[BusinessEntityID] (FK_Password_Person_BusinessEntityID)
        public virtual PasswordModel PasswordModel { get; set; }
        
        /// Child PersonPhones where [PersonPhone].[BusinessEntityID] point to this entity (FK_PersonPhone_Person_BusinessEntityID)
        public virtual ICollection<PersonPhoneModel> PersonPhonesModel { get; set; } = new HashSet<PersonPhoneModel>();
        
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

            if (obj is PersonModel) {
                PersonModel toCompare = (PersonModel)obj;
              if(toCompare != null)
              {
                  result = Equals(toCompare);
              }
            }

            return result;
        }
        
        public virtual bool Equals(PersonModel toCompare) {

        bool result = false;

            if (toCompare != null) {
                result = toCompare.BusinessEntityID == BusinessEntityID
;
            }

            return result;
        }
    }
}

