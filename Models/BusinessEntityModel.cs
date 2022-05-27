using PersonAPI.Entities;
using PersonAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonAPI.Models {
    
    
    public class BusinessEntityModel {
        
        protected internal int _businessEntityID;
        
        protected internal System.Guid _rowguid;
        
        protected internal System.DateTime _modifiedDate;
        
        public BusinessEntityModel() {
        }
        
        internal BusinessEntityModel(BusinessEntity entity) {
            this._businessEntityID = entity.BusinessEntityID;
            _rowguid = entity.Rowguid;
            this._modifiedDate = entity.ModifiedDate;
        }
        
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID {
            get {
                return this._businessEntityID;
            }
            set {
                this._businessEntityID = value;
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
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[BusinessEntityID] point to this entity (FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID)
        public virtual ICollection<BusinessEntityAddressModel> BusinessEntityAddressesModel { get; set; } = new HashSet<BusinessEntityAddressModel>();
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[BusinessEntityID] point to this entity (FK_BusinessEntityContact_BusinessEntity_BusinessEntityID)
        public virtual ICollection<BusinessEntityContactModel> BusinessEntityContactsModel { get; set; } = new HashSet<BusinessEntityContactModel>();
        
        /// Parent (One-to-One) BusinessEntity pointed by [Person].[BusinessEntityID] (FK_Person_BusinessEntity_BusinessEntityID)
        public virtual PersonModel PersonModel { get; set; }
        
        public override int GetHashCode() {
            int hash = 0;
            hash ^=Rowguid.GetHashCode();
            return hash;
        }
        
        public override string ToString() {
            return GetHashCode().ToString();
        }
    }
}

