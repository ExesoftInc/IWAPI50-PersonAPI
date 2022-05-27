using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class Password {
        
        [Key()]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Display(Name = "Password hash")]
        public string PasswordHash { get; set; }
        
        [Display(Name = "Password salt")]
        public string PasswordSalt { get; set; }
        
        [Display(Name = "Rowguid")]
        public System.Guid Rowguid { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Person pointed by [Password].([BusinessEntityID]) (FK_Password_Person_BusinessEntityID)
        public virtual Person Person { get; set; }
    }
}

