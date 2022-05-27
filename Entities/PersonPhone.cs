using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class PersonPhone {
        
        [Key()]
        [Column(Order=1)]
        [Display(Name = "Business entity ID")]
        public int BusinessEntityID { get; set; }
        
        [Key()]
        [Column(Order=2)]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }
        
        [Key()]
        [Column(Order=3)]
        [Display(Name = "Phone number type ID")]
        public int PhoneNumberTypeID { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        // Parent Person pointed by [PersonPhone].([BusinessEntityID]) (FK_PersonPhone_Person_BusinessEntityID)
        public virtual Person Person { get; set; }
        
        // Parent PhoneNumberType pointed by [PersonPhone].([PhoneNumberTypeID]) (FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID)
        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}

