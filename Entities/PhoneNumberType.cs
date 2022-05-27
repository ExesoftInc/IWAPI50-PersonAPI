using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class PhoneNumberType {
        
        [Key()]
        [Display(Name = "Phone number type ID")]
        public int PhoneNumberTypeID { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child PersonPhones where [PersonPhone].[PhoneNumberTypeID] point to this entity (FK_PersonPhone_PhoneNumberType_PhoneNumberTypeID)
        public virtual ICollection<PersonPhone> PersonPhones { get; set; } = new HashSet<PersonPhone>();
    }
}

