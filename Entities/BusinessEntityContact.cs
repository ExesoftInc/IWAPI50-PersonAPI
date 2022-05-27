using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class BusinessEntityContact {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Person ID")]
        public int PersonID { get; set; }
        
        [Key()]
        [Column(Order=3)]
        [Display(Name = "Contact type ID")]
        public int ContactTypeID { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent BusinessEntity pointed by [BusinessEntityContact].([BusinessEntityID]) (FK_BusinessEntityContact_BusinessEntity_BusinessEntityID)
        public virtual BusinessEntity BusinessEntity { get; set; }
        
        // Parent Person pointed by [BusinessEntityContact].([PersonID]) (FK_BusinessEntityContact_Person_PersonID)
        public virtual Person Person { get; set; }
        
        // Parent ContactType pointed by [BusinessEntityContact].([ContactTypeID]) (FK_BusinessEntityContact_ContactType_ContactTypeID)
        public virtual ContactType ContactType { get; set; }
    }
}

