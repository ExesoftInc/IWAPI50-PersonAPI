// ----------------------------------------------------------------------------------
// <copyright company="Exesoft Inc.">
//	This code was generated by Instant Web API code automation software (https://www.InstantWebAPI.com)
//	Copyright Exesoft Inc. © 2019.  All rights reserved.
// </copyright>
// ----------------------------------------------------------------------------------

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

