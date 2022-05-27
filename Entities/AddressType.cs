using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class AddressType {
        
        [Key()]
        [Display(Name = "Address type ID")]
        public int AddressTypeID { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child BusinessEntityAddresses where [BusinessEntityAddress].[AddressTypeID] point to this entity (FK_BusinessEntityAddress_AddressType_AddressTypeID)
        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddresses { get; set; } = new HashSet<BusinessEntityAddress>();
    }
}

