using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class Address {
        
        [Key()]
        [Display(Name = "Address ID")]
        public int AddressID { get; set; }
        
        [Display(Name = "Address line 1")]
        public string AddressLine1 { get; set; }
        
        [Display(Name = "Address line 2")]
        public string AddressLine2 { get; set; }
        
        [Display(Name = "City")]
        public string City { get; set; }
        
        [Display(Name = "State province ID")]
        public int StateProvinceID { get; set; }
        
        [Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[AddressID] point to this entity (FK_BusinessEntityAddress_Address_AddressID)
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; } = new HashSet<BusinessEntityAddress>();
        
        // Parent StateProvince pointed by [Address].([StateProvinceID]) (FK_Address_StateProvince_StateProvinceID)
        public virtual StateProvince StateProvince { get; set; }
    }
}

