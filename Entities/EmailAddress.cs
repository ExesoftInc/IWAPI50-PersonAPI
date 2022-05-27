using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class EmailAddress {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Email address ID")]
        public int EmailAddressID { get; set; }
        
        [Display(Name = "Email address")]
        public string EmailAddress_ { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Person pointed by [EmailAddress].([BusinessEntityID]) (FK_EmailAddress_Person_BusinessEntityID)
        public virtual Person Person { get; set; }
    }
}

