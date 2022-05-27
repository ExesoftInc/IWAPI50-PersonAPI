using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class BusinessEntityAddress {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Address ID")]
        public int AddressID { get; set; }
        
        [Key()]
        [Column(Order=3)]
        [Display(Name = "Address type ID")]
        public int AddressTypeID { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent BusinessEntity pointed by [BusinessEntityAddress].([BusinessEntityID]) (FK_BusinessEntityAddress_BusinessEntity_BusinessEntityID)
        public virtual BusinessEntity BusinessEntity { get; set; }
        
        // Parent Address pointed by [BusinessEntityAddress].([AddressID]) (FK_BusinessEntityAddress_Address_AddressID)
        public virtual Address Address { get; set; }
        
        // Parent AddressType pointed by [BusinessEntityAddress].([AddressTypeID]) (FK_BusinessEntityAddress_AddressType_AddressTypeID)
        public virtual AddressType AddressType { get; set; }
    }
}

