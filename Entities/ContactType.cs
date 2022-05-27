using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonAPI.Entities {
    
    
    public partial class ContactType {
        
        [Key()]
        [Display(Name = "Contact type ID")]
        public int ContactTypeID { get; set; }
        
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Display(Name = "Modified date")]
        public System.DateTime ModifiedDate { get; set; }
        
        /// Child BusinessEntityContacts where [BusinessEntityContact].[ContactTypeID] point to this entity (FK_BusinessEntityContact_ContactType_ContactTypeID)
        public virtual ICollection<BusinessEntityContact> BusinessEntityContacts { get; set; } = new HashSet<BusinessEntityContact>();
    }
}

